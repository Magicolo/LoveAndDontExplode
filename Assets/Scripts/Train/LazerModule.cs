using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LazerModule : ModuleBase
{
	public WeaponBase Weapon;

	public override void UpdateModule(ActivatorBase owner)
	{
		if (owner.Input.GetAction("Fire").GetKey())
		{
			if (Weapon.CanFire())
				Weapon.Fire();
		}
	}
}