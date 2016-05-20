﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo.Pooling;
using Pseudo.Injection;

namespace Pseudo.EntityFramework
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Pseudo/General/Entity")]
	public class EntityBehaviour : PMonoBehaviour, IPoolable
	{
		class EntityState
		{
			public bool Active;
			public bool Enabled;
			public bool[] ComponentStates;
		}

		public IEntity Entity
		{
			get { return entity; }
		}
		public EntityGroups Groups
		{
			get { return entity.Groups; }
			set { entity.Groups = value; }
		}
		public bool IsRoot
		{
			get { return parent == null; }
		}

		/// <summary>
		/// Groups that the linked Entity will have.
		/// </summary>
		[SerializeField]
		EntityGroups groups = null;
		/// <summary>
		/// Needed to determine if EntityBehaviour is the root of its hierarchy.
		/// </summary>
		[NonSerialized]
		EntityBehaviour parent;
		/// <summary>
		/// Needed to build the Entity hierarchy.
		/// </summary>
		[NonSerialized]
		EntityBehaviour[] children;
		/// <summary>
		/// Default components.
		/// </summary>
		IComponent[] components;
		/// <summary>
		/// Cached components on the same GameObject.
		/// </summary>
		ComponentBehaviourBase[] componentBehaviours;
		/// <summary>
		/// Some information about the initial state of the EntityBehaviour and its components.
		/// Used to reset the EntityBehaviour and its components to their initial state.
		/// </summary>
		EntityState initialState;

		IEntityManager entityManager;
		IEntity entity;

		protected override void OnEnable()
		{
			base.OnEnable();

			if (entity != null)
				entity.Active = true;
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			if (entity != null)
				entity.Active = false;
		}

		void OnDestroy()
		{
			Recycle(false);
		}

		[Inject]
		public void Initialize(IEntityManager entityManager)
		{
			Initialize(entityManager, false);
		}

		void CreateEntity()
		{
			if (entity != null)
				RecycleEntity();

			entity = entityManager.CreateEntity(groups, enabled);

			for (int i = 0; i < children.Length; i++)
				entity.AddChild(children[i].Entity);

			entity.AddAll(components);
			entity.AddAll(componentBehaviours);

			// Activate components
			for (int i = 0; i < componentBehaviours.Length; i++)
			{
				var component = componentBehaviours[i];

				if (component.Active && component.Entity != null)
					component.OnActivated();
			}
		}

		void RecycleEntity()
		{
			if (entity == null)
				return;

			// Deactivate components
			for (int i = 0; i < componentBehaviours.Length; i++)
			{
				var component = componentBehaviours[i];

				if (component.Active && component.Entity != null)
					component.OnDeactivated();
			}

			entityManager.RecycleEntity(entity);
			entity = null;
		}

		void Initialize(IEntityManager entityManager, bool fromRoot)
		{
			this.entityManager = entityManager;

			InitializeHierarchyIfNeeded();
			InitializeComponentsIfNeeded();

			// Initialize children only if this call comes from a root EntityBehaviour to ensure proper initialization of the Entity hierarchy.
			if (fromRoot || IsRoot)
			{
				// Initialize bottom up
				for (int i = 0; i < children.Length; i++)
					children[i].Initialize(entityManager, true);

				CreateEntity();
			}
		}

		void InitializeHierarchyIfNeeded()
		{
			if (children != null)
				return;

			parent = gameObject.GetComponent<EntityBehaviour>(HierarchyScopes.Parent);
			var childList = new List<EntityBehaviour>();
			PopulateChildren(transform, childList);
			children = childList.ToArray();
		}

		void InitializeComponentsIfNeeded()
		{
			if (components != null && componentBehaviours != null)
				return;

			components = new IComponent[]
			{
				new TransformComponent(transform),
				new GameObjectComponent(gameObject),
				new BehaviourComponent(this)
			};

			componentBehaviours = GetComponents<ComponentBehaviourBase>();
			SaveInitialState();
		}

		void Recycle(bool resetState)
		{
			// Recycle from bottom to top
			for (int i = 0; i < children.Length; i++)
			{
				var child = children[i];

				if (child != null)
					child.Recycle(resetState);
			}

			RecycleEntity();

			if (resetState)
				ResetToInitialState();
		}

		void SaveInitialState()
		{
			initialState = new EntityState
			{
				Active = gameObject.activeSelf,
				Enabled = enabled,
				ComponentStates = componentBehaviours.Convert(c => c.enabled)
			};

		}

		void ResetToInitialState()
		{
			gameObject.SetActive(initialState.Active);
			enabled = initialState.Enabled;

			for (int i = 0; i < initialState.ComponentStates.Length; i++)
				componentBehaviours[i].enabled = initialState.ComponentStates[i];
		}

		void PopulateChildren(Transform parent, List<EntityBehaviour> entities)
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				var child = parent.GetChild(i);
				var entity = child.GetComponent<EntityBehaviour>();

				if (entity == null)
					PopulateChildren(child, entities);
				else
					entities.Add(entity);
			}
		}

		void IPoolable.OnCreate() { }

		void IPoolable.OnRecycle()
		{
			Recycle(true);
		}
	}
}