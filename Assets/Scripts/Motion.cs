using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TimeComponent))]
public class Motion : MotionBase
{
	public float MoveSpeed = 5f;
	public float RotateSpeed = 5f;

	Rigidbody2D body;
	TimeComponent time;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		time = GetComponent<TimeComponent>();
	}

	public override void Move(Vector2 motion)
	{
		body.AccelerateTowards(motion * MoveSpeed, time.FixedDeltaTime);
	}

	public override void LookAt(float angle)
	{
		body.RotateTowards(angle, time.FixedDeltaTime * RotateSpeed);
	}
}