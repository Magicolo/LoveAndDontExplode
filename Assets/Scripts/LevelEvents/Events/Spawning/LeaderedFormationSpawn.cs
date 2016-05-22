using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]

public class LeaderedFormationSpawn : SpawnnerEvent
{
	[Inject(Cameras.Main)]
	Camera Cam;

	[Slider(-1, 2), Tooltip("Location Spawnned par rapport à L'écran. 0 = gauche, 1 = à droite.")]
	public float RatioXStart;

	[Min(0)]
	public int NbToSpawn;

	public FormationLeader Leader;
	public GameObject PrefabToSpawn;
	public FormationBase Formation;



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
		if (Leader == null)
		{
			Debug.LogError("Yo J'ai besoin dun leader.");
			return;
		}

		GameObject leaderGo = Spawn(Leader.gameObject, Cam, RatioXStart);
		FormationLeader leader = leaderGo.GetComponent<FormationLeader>();
		leader.Formation = Formation;
		leader.FormationCount = NbToSpawn;

		for (int i = 0; i < NbToSpawn; i++)
		{
			GameObject go = Spawn(PrefabToSpawn, Cam, RatioXStart);
			Vector3 p = Formation.GetFormationPosition(i, NbToSpawn);
			go.transform.position += p;

			var fmc = go.GetComponent<FormationMotionControler>();
			if (fmc)
			{
				fmc.FormationLeader = leader;
				fmc.formatId = i;
			}

		}


		enabled = false;

	}
}