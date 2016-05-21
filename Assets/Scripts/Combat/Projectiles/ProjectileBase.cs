using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class ProjectileBase : MonoBehaviour
{
	public abstract void Fire(Vector3 position, float angle);


	protected GameObject Spawn(GameObject prefab, Vector3 position)
	{
		GameObject go = GameObject.Instantiate(prefab);

		go.transform.position = position;

		return go;
	}
}