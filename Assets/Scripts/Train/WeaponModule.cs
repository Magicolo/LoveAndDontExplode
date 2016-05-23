using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class WeaponModule : ModuleBase
{
	public Turret Turret;
	public TimeComponent Time;

	public override void UpdateModule(ActivatorBase owner)
	{
		if (owner.Input.GetAction("Fire").GetKey())
		{
			if (Turret.Weapon.CanFire())
				Turret.Weapon.Fire();
		}

		float direction = -owner.Input.GetAction("MotionX").GetAxis();
		if (direction != 0)
		{
			float rotation = direction * Turret.RotationSpeed * Time.FixedDeltaTime;
			float z = (Turret.transform.rotation.eulerAngles.z + rotation).Clamp(Turret.AngleRange);
			Turret.transform.SetEulerAngles(z, Axes.Z);
		}
	}
}