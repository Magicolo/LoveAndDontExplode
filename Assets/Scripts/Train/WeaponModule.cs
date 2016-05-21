using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class WeaponModule : ModuleBase
{
	void Update()
	{
		if (owner == null)
			return;

		UpdateModule(owner);
	}

	public override void UpdateModule(ActivatorBase owner)
	{
		if (owner.Input.GetAction("Fire").GetKey())
		{
			// Fire Logic
		}
	}
}