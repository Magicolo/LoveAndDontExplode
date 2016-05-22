using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class WeaponModule : ModuleBase
{

	public Turret Turret;

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
			if (Turret.Weapon.CanFire())
				Turret.Weapon.Fire();
		}

		float direction = -owner.Input.GetAction("MotionX").GetAxis();
		if (direction != 0)
		{
			float rotation = direction * Turret.RotationSpeed * GetComponentInParent<TimeComponent>().DeltaTime;
			float z = Turret.transform.rotation.eulerAngles.z + rotation;
			z = z.Clamp(Turret.AngleRange);
			Turret.transform.rotation = Quaternion.Euler(0, 0, z);

		}
	}
}