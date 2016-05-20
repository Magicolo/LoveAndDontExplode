﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using Pseudo.Internal;

namespace Pseudo.Input
{
	[AddComponentMenu("Pseudo/General/Player Input")]
	public class PlayerInput : MonoBehaviour
	{
		public Players Player;
		public InputAction[] Actions = new InputAction[0];

		protected readonly Dictionary<string, InputAction> actions = new Dictionary<string, InputAction>();

		void Awake()
		{
			for (int i = 0; i < Actions.Length; i++)
			{
				var action = Actions[i];
				actions[action.Name] = action;
			}
		}

		public virtual InputAction GetAction(string name)
		{
			InputAction action;

			if (!actions.TryGetValue(name, out action))
				throw new ArgumentException(string.Format("Action named {0} was not found.", name));

			return action;
		}
	}
}