using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LeaderFellowerSpawning : MonoBehaviour
{
	public FormationLeader Leader;

	void Spawing(GameObject go)
	{
		foreach (var controler in go.GetComponentsInChildren<FormationMotionControler>())
		{
			controler.FormationLeader = Leader;
		}
	}
}