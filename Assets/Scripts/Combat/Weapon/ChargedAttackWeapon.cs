using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ChargedAttackWeapon : SimpleWeapon
{

	public MinMax DamageRange;
	public MinMax ScaleRange;

	public float MaxChargeTime;

	float lastFire;


	public override void Fire()
	{
		if (CanFire())
		{
			//Nous avons reloader donc on remet les bullets
			if (currentAmmo == 0)
				currentAmmo = (int)Ammo.GetRandom();

			currentAmmo--;

			GameObject[] bullets = Projectile.Fire(WeaponRoot.position, WeaponRoot.rotation.eulerAngles.z);


			float timeWaited = Time.Time - lastFire;
			lastFire = Time.Time;

			float timeRatio = timeWaited / MaxChargeTime;

			foreach (var bullet in bullets)
			{
				bullet.transform.localScale *= ScaleRange.GetAtT(timeRatio);
				foreach (var damager in bullet.GetComponents<DamagerBase>())
				{
					damager.DamageToCause.Damage = DamageRange.GetAtT(timeRatio);
				}
			}


			if (currentAmmo == 0)
			{
				t = Time.Time + Cooldown.GetRandom();
			}
			else
			{
				t = Time.Time + FireRate;
			}
		}
	}
}