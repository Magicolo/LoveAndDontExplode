using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Input;
using Pseudo.Injection;

public class InputController : ControllerBase
{
	[Header("Uses 'MotionX' and 'MotionY' axes.")]
	public InputComponent Input;
	public MotionBase Motion;

	void FixedUpdate()
	{
		var direction = new Vector2(Input.GetAction("MotionX").GetAxis(), Input.GetAction("MotionY").GetAxis()).normalized;

		if (direction != Vector2.zero)
		{
			Motion.Move(direction);
			Motion.RotateTo(direction.Angle());
		}
	}
}
