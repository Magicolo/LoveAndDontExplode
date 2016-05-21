using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FreezeNearTarget : MonoBehaviour
{
	public TransformTarget Target;
	public float Epsilon;

	void Update()
	{
		if (Vector3.Distance(Target.Target.position, transform.position) < Epsilon)
		{
			gameObject.AddComponent<FreezeMotion>();
			enabled = false;
		}
	}

}