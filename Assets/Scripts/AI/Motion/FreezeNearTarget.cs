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

	public ControllerBase[] ActivateControler;
	public ControllerBase[] DeactivateControlers;

	void Update()
	{
		if (Vector3.Distance(Target.Target.position, transform.position) < Epsilon)
		{
			gameObject.AddComponent<FreezeMotion>();
			enabled = false;

			foreach (var c in DeactivateControlers)
				c.enabled = false;

			foreach (var c in ActivateControler)
				c.enabled = true;
		}



	}

}