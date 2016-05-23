using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ActivationBasedWeapon : SimpleWeapon
{
	public float AnimationSpeed = 5f;
	public LineRenderer Line;

	bool isFiring;
	bool lastFiring;


	protected override void FireProjectiles()
	{
		isFiring = true;
		lastFiring = true;
	}

	protected override void Update()
	{
		base.Update();

		Line.gameObject.SetActive(isFiring);

		if (isFiring)
			UpdateAnimation();

		isFiring = lastFiring;
		lastFiring = false;
	}

	void UpdateAnimation()
	{
		var offset = Line.material.mainTextureOffset;
		offset.x -= AnimationSpeed * Time.DeltaTime;
		offset.x = offset.x.Wrap(0f, 1f);
		Line.material.mainTextureOffset = offset;
	}

}