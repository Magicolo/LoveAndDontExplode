using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class SpawnLevelEvent : ILevelEvent
{

	[Inject(Cameras.Main)]
	Camera Cam;

	[Slider(0, 1)]
	public float RatioPositionStart;

	public GameObject PrefabToSpawn;

	internal override void Activate()
	{
		GameObject go = GameObject.Instantiate(PrefabToSpawn);
		Vector3 p = Cam.ViewportToWorldPoint(new Vector3(RatioPositionStart, transform.localPosition.y / 100,0));
		// TODO offsetSelon le sprite;
		go.transform.position = p;
	}
}