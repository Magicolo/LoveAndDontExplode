using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpawningMotionTargetGivers : MonoBehaviour
{
	public Transform Target;

	void Spawing(GameObject go)
	{
		foreach (var controler in go.GetComponents<MoveTowardsTargetMotionControler>())
		{
			controler.Target.Target = Target;
		}
	}
}