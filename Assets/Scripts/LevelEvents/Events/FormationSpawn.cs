using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]
public class FormationSpawn : SpawnnerEvent
{
	[Inject(Cameras.Main)]
	Camera Cam;

	[Slider(-1, 2), Tooltip("Location Spawnned par rapport à L'écran. 0 = gauche, 1 = à droite.")]
	public float RatioXStart;

	[Min(0)]
	public int NbToSpawn;

	public GameObject PrefabToSpawn;
	public FormationComponent Formation;



	internal override void Activate()
	{
		Spawn();
	}

	private void Spawn()
	{
		if (Formation == null)
		{
			Debug.LogError("Yo J'ai besoin dun formation.");
			return;
		}

		for (int i = 0; i < NbToSpawn; i++)
		{
			GameObject go = Spawn(PrefabToSpawn, Cam, RatioXStart);
			Vector3 p = Formation.GetFormationPosition(i, NbToSpawn);
			go.transform.position += p;
		}


		enabled = false;

	}
}