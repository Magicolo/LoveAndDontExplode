using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class MotionModule : ModuleBase
{
	[Header("Uses 'MotionX' action.")]
	public MotionBase ShipMotion;

	void FixedUpdate()
	{
		if (owner == null)
			return;

		UpdateModule(owner);
	}

	public override void UpdateModule(ActivatorBase owner)
	{
		var input = owner.Input.GetAction("MotionX").GetAxis();
		ShipMotion.Move(new Vector2(input, 0f));
	}
}
