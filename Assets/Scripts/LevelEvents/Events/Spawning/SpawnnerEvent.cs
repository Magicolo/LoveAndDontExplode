using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class SpawnnerEvent : ILevelEvent
{
	protected GameObject Spawn(GameObject PrefabToSpawn, Camera cam, float x)
	{
		float levelHeight = GetComponentInParent<LevelEvents>().LevelHeight;
		GameObject go = GameObject.Instantiate(PrefabToSpawn);
		Vector3 p = cam.ViewportToWorldPoint(new Vector3(x, transform.localPosition.y / levelHeight, -cam.transform.position.z));
		//PDebug.Log(new Vector3(RatioXStart, transform.localPosition.y / levelHeight, 0), p);
		// TODO offsetSelon le sprite;
		go.transform.position = p;

		gameObject.SendMessage("Spawing", go, SendMessageOptions.DontRequireReceiver);
		return go;
	}
}