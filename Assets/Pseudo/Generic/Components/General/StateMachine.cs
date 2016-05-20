﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.EntityFramework;
using Pseudo.Pooling;
using Pseudo.Communication;

namespace Pseudo
{
	[MessageEnum]
	public enum StateMachineMessages
	{
		OnStateEnter,
		OnStateExit
	}

	public class StateMachine : ComponentBehaviourBase, IMessageable
	{
		[Serializable]
		public struct StateData
		{
			public Message Message;
			public EntityBehaviour State;
		}

		public StateData[] States = new StateData[0];

		EntityBehaviour currentState;

		public override void OnAdded()
		{
			base.OnAdded();

			for (int i = 0; i < States.Length; i++)
			{
				var state = States[i];

				if (state.State != null)
					state.State.gameObject.SetActive(false);
			}
		}

		void SwitchState(EntityBehaviour state)
		{
			if (currentState != null)
			{
				Entity.SendMessage(StateMachineMessages.OnStateExit, HierarchyScopes.Children | HierarchyScopes.Self);
				currentState.gameObject.SetActive(false);
			}

			currentState = state;

			if (currentState != null)
			{
				currentState.gameObject.SetActive(true);
				Entity.SendMessage(StateMachineMessages.OnStateEnter, HierarchyScopes.Children | HierarchyScopes.Self);
			}
		}

		void IMessageable.OnMessage<TId>(TId message)
		{
			for (int i = 0; i < States.Length; i++)
			{
				var state = States[i];

				if (state.Message.Equals(message))
					SwitchState(state.State);
			}
		}
	}
}