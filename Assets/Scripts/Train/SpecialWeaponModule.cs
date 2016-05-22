using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpecialWeaponModule : WeaponModule
{
	public override void UpdateModule(ActivatorBase owner)
	{
		if (Turret.Weapon.CanFire())
			Turret.Weapon.Fire();

		owner.Deactivate();
	}
}