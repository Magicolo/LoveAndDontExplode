using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LookAtTarget : MonoBehaviour
{
	public TransformTarget Target;

	void Update()
	{
		transform.LookAt(Target.Target, Vector3.up);
	}

}