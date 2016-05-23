using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Turret : MonoBehaviour
{
	public MinMax AngleRange;
	public float RotationSpeed = 60f;
	public WeaponBase Weapon;
}