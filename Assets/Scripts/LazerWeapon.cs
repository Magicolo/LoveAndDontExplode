using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LazerWeapon : WeaponBase
{
	public float AnimationSpeed = 5f;
	public LineRenderer Line;
	public TimeComponent Time;

	bool isFiring;
	bool lastFiring;

	public override bool CanFire()
	{
		return true;
	}

	public override void Fire()
	{
		isFiring = true;
		lastFiring = true;
	}

	void Update()
	{
		Line.enabled = isFiring;

		if (isFiring)
		{
			UpdatePhysics();
			UpdateAnimation();
		}

		isFiring = lastFiring;
		lastFiring = false;
	}

	void UpdatePhysics()
	{
		var size = new Vector3(13f, 1600f);
		PDebug.LogMethod(Physics2D.BoxCastAll(transform.position + size * 0.5f, size, 0f, transform.right).Convert(h => h.collider));
	}

	void UpdateAnimation()
	{
		var offset = Line.material.mainTextureOffset;
		offset.x -= AnimationSpeed * Time.DeltaTime;
		offset.x = offset.x.Wrap(0f, 1f);
		Line.material.mainTextureOffset = offset;
	}
}
