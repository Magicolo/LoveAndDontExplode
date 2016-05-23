using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FixedVectorMotionControler : ControllerBase
{
	public MotionBase Motion;
	public Vector3 Target;
	public float Damping = 1;

	public Axes LockedAxes;

	void FixedUpdate()
	{
		var v = (Target - Motion.transform.position).SetValues(0, LockedAxes);
		var vn = v.normalized * Damping;
		Motion.Move(vn);
		Motion.RotateTo(Mathf.Rad2Deg * Mathf.Atan2(v.normalized.y, v.normalized.x));
	}
}