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
	[Header("Uses 'MotionX' and 'MotionY' actions.")]
	public InputComponent Input;
	public MotionBase Motion;

	// It seems that FixedUpdate can be called before Start on other scripts and injection occurs in Start, which sucks...
	bool skipHack;

	void FixedUpdate()
	{
		if (!skipHack)
		{
			skipHack = true;
			return;
		}

		var direction = new Vector2(Input.GetAction("MotionX").GetAxis(), Input.GetAction("MotionY").GetAxis()).normalized;

		if (direction != Vector2.zero)
		{
			Motion.Move(direction);
			Motion.RotateTo(direction.Angle());
		}
	}
}
