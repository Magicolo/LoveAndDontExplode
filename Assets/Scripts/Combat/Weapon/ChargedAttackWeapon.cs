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

	protected override void FireProjectiles()
	{
		GameObject[] bullets = Projectile.Fire(WeaponRoot.position, WeaponRoot.rotation.eulerAngles.z);
		float timeWaited = Time.Time - lastFired;

		float timeRatio = timeWaited / MaxChargeTime;
		timeRatio = timeRatio * timeRatio;

		foreach (var bullet in bullets)
		{
			bullet.transform.localScale *= ScaleRange.GetAtT(timeRatio);
			foreach (var damager in bullet.GetComponents<DamagerBase>())
			{
				damager.DamageToCause.Damage = DamageRange.GetAtT(timeRatio);
			}
		}
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
		float timeWaited = Time.Time - lastFired;
		return timeWaited / MaxChargeTime;
	}
}