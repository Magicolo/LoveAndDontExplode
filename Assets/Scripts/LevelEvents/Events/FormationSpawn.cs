using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]
public class FormationSpawn : ILevelEvent
{
	[Inject(Cameras.Main)]
	Camera Cam;

	[Slider(0, 1), Tooltip("Location Spawnned par rapport à L'écran. 0 = gauche, 1 = à droite.")]
	public float RatioXStart;

	public GameObject PrefabToSpawn;



	internal override void Activate()
	{
		Spawn();
	}

	private void Spawn()
	{
		float levelHeight = GetComponentInParent<LevelEvents>().LevelHeight;
		GameObject go = GameObject.Instantiate(PrefabToSpawn);
		Vector3 p = Cam.ViewportToWorldPoint(new Vector3(RatioXStart, transform.localPosition.y / levelHeight, -Cam.transform.position.z));
		//PDebug.Log(new Vector3(RatioXStart, transform.localPosition.y / levelHeight, 0), p);
		// TODO offsetSelon le sprite;
		go.transform.position = p;
		enabled = false;

	}
}