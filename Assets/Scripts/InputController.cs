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
	public float Speed = 5f;
	public Players Player;
	public MotionBase Motion;

	[Inject]
	IInputManager inputManager;
	PlayerInput player;

	protected override void Start()
	{
		base.Start();

		player = inputManager.GetAssignedInput(Player);
	}

	void Update()
	{
		var direction = new Vector2(player.GetAction("MotionX").GetAxis(), player.GetAction("MotionY").GetAxis()).normalized;
		Motion.Move(direction * Speed);
	}
}
