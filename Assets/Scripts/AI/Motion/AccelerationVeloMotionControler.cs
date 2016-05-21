using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class AccelerationVeloMotionControler : ControllerBase
{
	public MotionBase Motion;

	public Vector2 StartingVelocity;

	public MinMax XAcceleration;
	public MinMax YAcceleration;

	Vector2 velocity;
	float xa;
	float ya;

	override protected void Start()
	{
		base.Start();
		velocity = StartingVelocity;
		xa = XAcceleration.GetRandom();
		ya = YAcceleration.GetRandom();
	}
	void FixedUpdate()
	{
		velocity += new Vector2(xa, ya) * GetComponent<TimeComponent>().DeltaTime;

		Motion.Move(velocity.normalized);
	}
}