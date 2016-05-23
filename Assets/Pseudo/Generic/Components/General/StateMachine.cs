using UnityEngine;
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

	public class StateMachine : PMonoBehaviour, IMessageable
	{
		[Serializable]
		public struct StateData
		{
			public Message Message;
			public GameObject State;
		}

		public StateData[] States = new StateData[0];

		GameObject currentState;

		void OnCreate()
		{
			for (int i = 0; i < States.Length; i++)
			{
				var state = States[i];

				if (state.State != null)
					state.State.gameObject.SetActive(false);
			}
		}

		void SwitchState(GameObject state)
		{
			if (currentState != null)
			{
				SendMessage("OnStateExit", HierarchyScopes.Children | HierarchyScopes.Self);
				currentState.gameObject.SetActive(false);
			}

			currentState = state;

			if (currentState != null)
			{
				currentState.gameObject.SetActive(true);
				SendMessage("OnStateEnter", HierarchyScopes.Children | HierarchyScopes.Self);
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