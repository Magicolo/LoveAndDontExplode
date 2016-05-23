using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class MoveTowardsTargetMotionControler : ControllerBase
{
	public MotionBase Motion;
	public TransformTarget Target;

	public Axes LockedAxes;

	void FixedUpdate()
	{
		var v = (Target.Target.position - Motion.transform.position).SetValues(0, LockedAxes);
		Motion.Move(v.normalized);
	}
}