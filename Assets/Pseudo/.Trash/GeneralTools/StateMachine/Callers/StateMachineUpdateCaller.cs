﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Internal
{
	[AddComponentMenu("")]
	public class StateMachineUpdateCaller : StateMachineCaller
	{
		void Update()
		{
			if (machine.IsActive)
				machine.OnUpdate();
		}
	}
}