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

	void FixedUpdate()
	{
		Motion.Move((Target.Target.position - Motion.transform.position).normalized);
	}
}