using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TransformTargetGiver : PMonoBehaviour
{
	public String Key;
	public Transform Target;

	void Spawing(GameObject go)
	{
		foreach (var controler in go.GetComponentsInChildren<TransformTarget>())
		{
			if (controler.TargetKey == Key)
				controler.Target = Target;
		}
	}
}