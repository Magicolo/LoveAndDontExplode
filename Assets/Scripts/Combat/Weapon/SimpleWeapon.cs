using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class SimpleWeapon : WeaponBase
{

	public ProjectileBase Projectile;
	public Transform WeaponRoot;

	public MinMax Cooldown;
	public MinMax Ammo = new MinMax(1, 1);
	public MinMax FireRate = new MinMax(0.5f, 1);

	[Disable]
	public float currentAmmo;

	float t;

	public override bool CanFire()
	{
		return GetComponent<TimeComponent>().Time > t;
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
				t = GetComponent<TimeComponent>().Time + Cooldown.GetRandom() + FireRate.GetRandom();
			}
			else
			{
				t = GetComponent<TimeComponent>().Time + FireRate.GetRandom();
			}
		}
	}
}