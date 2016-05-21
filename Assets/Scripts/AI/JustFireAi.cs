using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class JustFireAi : MonoBehaviour
{

	public WeaponBase Weapon;

	void Update()
	{
		if (Weapon.CanFire())
			Weapon.Fire();
	}
}