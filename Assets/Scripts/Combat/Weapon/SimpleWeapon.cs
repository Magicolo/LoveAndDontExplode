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
	public float FireRate = 0.5f;

	[Disable]
	public int currentAmmo;

	int currentMaxAmmo = 1;
	protected bool reloading = true;

	protected float t;
	protected float lastFired;

	void Start()
	{
		currentMaxAmmo = (int)Ammo.GetRandom();
		currentAmmo = currentMaxAmmo;
	}

	protected virtual void Update()
	{
		if (reloading)
		{
			if (CanFire())
			{
				reloading = false;
				currentAmmo = currentMaxAmmo;
			}
			else
				currentAmmo = (int)(currentMaxAmmo * getCoolDownRatio());
		}
	}

	public override bool CanFire()
	{
		return Time.Time > t;
	}

	public override void Fire()
	{
		if (CanFire())
		{
			FireProjectiles();
			currentAmmo--;

			lastFired = Time.Time;

			if (currentAmmo <= 0)
			{
				reloading = true;
				currentMaxAmmo = (int)Ammo.GetRandom();
				t = Time.Time + Cooldown.GetRandom() + FireRate;
			}
			else
			{
				t = Time.Time + FireRate;
			}
		}
	}

	protected virtual void FireProjectiles()
	{
		Projectile.Fire(WeaponRoot.position, WeaponRoot.rotation.eulerAngles.z);
	}

	public override float getCoolDownRatio()
	{
		if (CanFire())
			return 1;

		float r = (Time.Time - lastFired) / (t - lastFired);
		return Mathf.Clamp(r, 0, 1);
	}

	public override float getAmmoRatio()
	{
		return (float)(currentAmmo) / (float)currentMaxAmmo;
	}
}