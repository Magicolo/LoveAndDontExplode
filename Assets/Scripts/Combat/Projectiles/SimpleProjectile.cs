using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SimpleProjectile : ProjectileBase
{
	public GameObject Prefab;
	public override GameObject[] Fire(Vector3 position, float angle)
	{
		GameObject[] bullet = new GameObject[1];

		GameObject go = Spawn(Prefab, position);
		go.transform.SetPosition(0, Axes.Z);
		go.transform.rotation = Quaternion.Euler(0, 0, angle);
		bullet[0] = go;

		return bullet;
	}
}