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
	public Players Player;
	public MotionBase Motion;

	[Inject]
	IInputManager inputManager = null;
	PlayerInput input;

	protected override void Start()
	{
		base.Start();

		input = inputManager.GetAssignedInput(Player);
	}

	void FixedUpdate()
	{
		var direction = new Vector2(input.GetAction("MotionX").GetAxis(), input.GetAction("MotionY").GetAxis()).normalized;

		if (direction != Vector2.zero)
		{
			Motion.Move(direction);
			Motion.LookAt(direction.Angle());
		}
	}
}
