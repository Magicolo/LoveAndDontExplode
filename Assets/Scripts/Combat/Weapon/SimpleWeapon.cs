using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SimpleWeapon : WeaponBase
{
	public TimeComponent Time;
	public ProjectileBase Projectile;
	public Transform WeaponRoot;

	public MinMax Cooldown;
	public MinMax Ammo = new MinMax(1, 1);
	public MinMax FireRate = new MinMax(0.5f, 1);

	[Disable]
	public float currentAmmo;

	protected float t;

	public override bool CanFire()
	{
		return Time.Time > t;
	}

	public override void Fire()
	{
		if (CanFire())
		{
			//Nous avons reloader donc on remet les bullets
			if (currentAmmo == 0)
				currentAmmo = Ammo.GetRandom();

			currentAmmo--;

			Projectile.Fire(WeaponRoot.position, WeaponRoot.rotation.eulerAngles.z);
			if (currentAmmo == 0)
			{
				t = Time.Time + Cooldown.GetRandom() + FireRate.GetRandom();
			}
			else
			{
				t = Time.Time + FireRate.GetRandom();
			}
		}
	}
}