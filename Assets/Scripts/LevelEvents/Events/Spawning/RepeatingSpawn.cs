using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]
public class RepeatingSpawn : SpawnnerEvent
{
	[Inject(Cameras.Main)]
	Camera Cam;

	public SpawnnerEvent EventToRepeat;
	public MinMax Frequency;
	public MinMax Times;


	float t;
	int spawnRemainning;
	bool spawns = false;

	TimeComponent Time { get { return GetComponent<TimeComponent>(); } }

	internal override void Activate()
	{
		if (EventToRepeat != null)
		{
			spawnRemainning = (int)Times.GetRandom();
			t = Time.Time;
			spawns = true;
		}

	}

	void Update()
	{
		if (spawns && Time.Time >= t)
		{
			EventToRepeat.enabled = true;
			spawnRemainning--;
			if (spawnRemainning <= 0)
				spawns = false;
			t = Time.Time + Frequency.GetRandom();

			EventToRepeat.Activate();
		}
	}
}