using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class AutoKeepFireWeapon : SimpleWeapon
{

	bool justFire = false;
	protected override void FireProjectiles()
	{
		justFire = true;
		base.FireProjectiles();
	}

	protected override void Update()
	{
		base.Update();

		if (!reloading && justFire)
		{
			Fire();
			if (currentAmmo <= 0)
				justFire = false;
		}
	}
}