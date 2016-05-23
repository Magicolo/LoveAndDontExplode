using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class BulletShaderFeeder : MonoBehaviour
{
	private Material Mat;
	public WeaponBase Weapon;

	void Start()
	{
		Mat = GetComponent<MeshRenderer>().material;
	}
	void Update()
	{
		Mat.SetFloat("_CoolDownRatio", Weapon.getCoolDownRatio());
		Mat.SetFloat("_AmmoRatio", Weapon.getAmmoRatio());
	}
}