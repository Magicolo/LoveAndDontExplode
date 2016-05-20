﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

namespace Pseudo.EntityFramework
{
	[RequireComponent(typeof(EntityBehaviour))]
	public abstract class ComponentBehaviourBase : PMonoBehaviour, IComponent
	{
		public bool Active
		{
			get { return active; }
			set { SetActive(value); }
		}
		public IEntity Entity
		{
			get { return entity; }
			set { entity = value; }
		}

		IEntity entity;
		bool active;

		protected override void OnEnable()
		{
			base.OnEnable();

			SetActive(true);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			SetActive(false);
		}

		void SetActive(bool active)
		{
			if (entity == null)
				this.active = active;
			else if (this.active != active)
			{
				this.active = active;

				if (this.active)
					OnActivated();
				else
					OnDeactivated();
			}
		}

		public virtual void OnAdded() { }
		public virtual void OnRemoved() { }
		public virtual void OnActivated() { }
		public virtual void OnDeactivated() { }
		public virtual void OnEntityActivated() { }
		public virtual void OnEntityDeactivated() { }
	}
}