using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpawnOnStart : PMonoBehaviour
{
	public GameObject Prefab;

	protected override void Start()
	{
		base.Start();

		var instance = Instantiate(Prefab);

		instance.transform.parent = transform;
		instance.transform.position = transform.position;
	}
}