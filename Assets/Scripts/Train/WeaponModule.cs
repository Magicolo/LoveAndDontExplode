using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class WeaponModule : ModuleBase
{
	public override void UpdateModule(ActivatorBase owner)
	{
		if (owner.Input.GetAction("Fire").GetKey())
		{
			// Fire Logic
		}
	}
}