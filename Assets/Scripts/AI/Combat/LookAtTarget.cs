using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LookAtTarget : MonoBehaviour
{
	public TransformTarget Target;

	Vector3 tp { get { return Target.Target.position; } }

	void Update()
	{
		float angle = Mathf.Rad2Deg * Mathf.Atan2(tp.y - transform.position.y, tp.x - transform.position.x);
		transform.RotateTowards(angle, 0.1f, Axes.Z);
	}

}