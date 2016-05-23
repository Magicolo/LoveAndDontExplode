using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class FixedVectorMotionControler : ControllerBase
{
	public MotionBase Motion;
	public Vector3 Target;
	public float Damping;

	public Axes LockedAxes;

	void FixedUpdate()
	{
		var v = (Target - Motion.transform.position).SetValues(0, LockedAxes);
		Motion.Move(v.normalized * Damping);
	}
}