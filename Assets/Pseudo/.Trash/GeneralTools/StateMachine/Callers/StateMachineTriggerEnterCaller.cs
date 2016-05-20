﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Internal
{
	[AddComponentMenu("")]
	public class StateMachineTriggerEnterCaller : StateMachineCaller
	{
		void OnTriggerEnter(Collider collision)
		{
			if (machine.IsActive)
				machine.TriggerEnter(collision);
		}
	}
}