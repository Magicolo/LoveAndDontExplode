using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TestFireWeapon : MonoBehaviour
{
	public KeyCode TriggerKey;

	[Button("FIRE", "Fire")]
	public bool fire;

	public WeaponBase Weapon;

	void Update()
	{
		if (Input.GetKey(TriggerKey))
			if (Weapon.CanFire())
				Weapon.Fire();
	}

	void Fire()
	{
		if (Weapon.CanFire())
			Weapon.Fire();
	}

}