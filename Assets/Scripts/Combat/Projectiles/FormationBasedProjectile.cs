using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FormationBasedProjectile : ProjectileBase
{

	public FormationBase Formation;
	public GameObject Prefab;

	public int NbSpawn;

	public bool RotateTowardFormation;

	public override GameObject[] Fire(Vector3 position, float angle)
	{
		GameObject[] bullets = new GameObject[NbSpawn];
		for (int i = 0; i < NbSpawn; i++)
		{
			Vector3 p = Formation.GetFormationPosition(i, NbSpawn);
			GameObject go = Spawn(Prefab, position + p);

			if (RotateTowardFormation)
				go.transform.Rotate(Mathf.Rad2Deg * Mathf.Atan2(p.y, p.x) + angle, Axes.Z);

			bullets[i] = go;

		}

		return bullets;
	}
}