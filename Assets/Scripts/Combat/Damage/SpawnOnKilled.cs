using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpawnOnKilled : PMonoBehaviour
{
	public GameObject ToSpawn;

	void OnKilled()
	{
		var spawn = Instantiate(ToSpawn);
		spawn.transform.SetPosition(transform.position, Axes.XY);
		spawn.transform.rotation = transform.rotation;
	}
}
