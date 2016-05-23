using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class JustTurnMotionControler : ControllerBase
{
	public MotionBase Motion;
	public float Direction;

	void FixedUpdate()
	{
		Motion.Rotate(Direction);
	}
}