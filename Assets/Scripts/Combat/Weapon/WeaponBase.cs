using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class WeaponBase : MonoBehaviour
{


	public abstract bool CanFire();

	public abstract void Fire();
	public abstract float getCoolDownRatio();
	public abstract float getAmmoRatio();
}