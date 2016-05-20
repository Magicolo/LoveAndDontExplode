using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]
public class SpawnLevelEvent : ILevelEvent
{

	[Inject(Cameras.Main)]
	Camera Cam;

	[Slider(0, 1), Tooltip("Location Spawnned par rapport à L'écran. 0 = gauche, 1 = à droite.")]
	public float RatioXStart;

	[Min(0), Tooltip("Spawn every X")]
	public float SpawnFrequency = 1;

	public int SpawnQuantity = 1;

	int spawned;
	float nextSpawn = -1;

	public GameObject PrefabToSpawn;

	internal override void Activate()
	{
		Spawn();
	}

	void Update()
	{
		if (GetComponent<TimeComponent>().Time >= nextSpawn)
		{
			Spawn();
		}
	}

	private void Spawn()
	{
		float levelHeight = GetComponentInParent<LevelEvents>().LevelHeight;
		GameObject go = GameObject.Instantiate(PrefabToSpawn);
		Vector3 p = Cam.ViewportToWorldPoint(new Vector3(RatioXStart, transform.localPosition.y / levelHeight, -Cam.transform.position.z));
		PDebug.Log(new Vector3(RatioXStart, transform.localPosition.y / levelHeight, 0), p);
		// TODO offsetSelon le sprite;
		go.transform.position = p;
		nextSpawn = GetComponent<TimeComponent>().Time + SpawnFrequency;
		spawned++;
		if (spawned >= SpawnQuantity)
			enabled = false;
	}
}