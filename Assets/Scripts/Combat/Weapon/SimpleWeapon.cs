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

	[Min(0)]
	public float Cooldown;

	[Min(1)]
	public float Ammo = 1;

	[Min(0.0001f)]
	public float FireRate = 1;

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
				currentAmmo = Ammo;

			currentAmmo--;

			Projectile.Fire(WeaponRoot.position, WeaponRoot.rotation.eulerAngles.z);
			if (currentAmmo == 0)
			{
				t = GetComponent<TimeComponent>().Time + Cooldown + FireRate;
			}
			else
			{
				t = GetComponent<TimeComponent>().Time + FireRate;
			}
		}
	}
}