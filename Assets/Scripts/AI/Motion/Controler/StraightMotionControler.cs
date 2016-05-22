using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class StraightMotionControler : ControllerBase
{
	public MotionBase Motion;

	void FixedUpdate()
	{
		Motion.Move(transform.right);
	}
}