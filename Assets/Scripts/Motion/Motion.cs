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
	public bool Accelerate = true;

	Rigidbody2D body;
	Rigidbody2D inherit;
	TimeComponent time;
	Vector2 velocity;
	Vector2 lastPosition;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		time = GetComponent<TimeComponent>();
	}

	protected override void Start()
	{
		base.Start();

		inherit = this.GetComponent<Rigidbody2D>(HierarchyScopes.Ancestors);

		if (inherit != null)
			lastPosition = inherit.position;
	}

	void FixedUpdate()
	{
		if (inherit != null)
		{
			var delta = inherit.position - lastPosition;
			body.MovePosition(body.position + velocity + delta);
			lastPosition = inherit.position;
			velocity *= 1f - Mathf.Clamp01(body.drag / 100f);
		}
		else if (body.isKinematic)
			body.velocity *= 1f - Mathf.Clamp01(body.drag / 100f);
	}

	public override void Move(Vector2 motion)
	{
		if (GetComponent<FreezeMotion>() == null)
		{
			if (Accelerate)
			{
				if (inherit == null)
					body.AccelerateTowards(motion * MoveSpeed, time.FixedDeltaTime);
				else
					velocity += motion * MoveSpeed * time.FixedDeltaTime;
			}
			else
			{
				if (inherit == null)
					body.SetVelocity(motion * MoveSpeed);
				else
					velocity = motion * MoveSpeed * time.FixedDeltaTime;
			}
		}
	}

	public override void MoveTo(Vector2 motion)
	{
		if (GetComponent<FreezeMotion>() == null)
			body.TranslateTowards(motion, time.FixedDeltaTime * MoveSpeed);
	}

	public override void Rotate(float angle)
	{
		if (GetComponent<FreezeMotion>() == null)
			body.Rotate(angle * RotateSpeed * time.FixedDeltaTime);
	}

	public override void RotateTo(float angle)
	{
		if (GetComponent<FreezeMotion>() == null)
			body.RotateTowards(angle, time.FixedDeltaTime * RotateSpeed);
	}
}