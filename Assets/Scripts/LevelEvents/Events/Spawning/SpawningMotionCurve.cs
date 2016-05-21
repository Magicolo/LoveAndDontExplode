using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpawningMotionCurve : MonoBehaviour
{
	public MotionCurvePrefab MotionCurvePrefab;

	void Spawing(GameObject go)
	{
		foreach (var controler in go.GetComponents<MotionCurveMotionControler>())
		{
			controler.MotionCurve = MotionCurvePrefab;
		}
	}
}