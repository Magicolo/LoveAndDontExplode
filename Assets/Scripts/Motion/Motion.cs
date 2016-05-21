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
	Vector2 velocity;
	TimeComponent time;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		time = GetComponent<TimeComponent>();
	}

	public override void Move(Vector2 motion, bool instant = false)
	{
		if (GetComponent<FreezeMotion>() == null)
		{
			if (instant)
				body.Translate(motion);
			else
				body.AccelerateTowards(motion * MoveSpeed, time.FixedDeltaTime);
		}
	}

	public override void MoveTo(Vector2 motion, bool instant = false)
	{
		if (GetComponent<FreezeMotion>() == null)
		{
			if (instant)
				body.position = motion;
			else
				body.TranslateTowards(motion, time.FixedDeltaTime * MoveSpeed);
		}
	}

	public override void Rotate(float angle, bool instant = false)
	{
		if (GetComponent<FreezeMotion>() == null)
		{
			if (instant)
				body.Rotate(angle);
			else
				body.Rotate(angle * RotateSpeed * time.FixedDeltaTime);
		}
	}

	public override void RotateTo(float angle, bool instant = false)
	{
		if (GetComponent<FreezeMotion>() == null)
		{
			if (instant)
				body.rotation = angle;
			else
				body.RotateTowards(angle, time.FixedDeltaTime * RotateSpeed);
		}
	}
}