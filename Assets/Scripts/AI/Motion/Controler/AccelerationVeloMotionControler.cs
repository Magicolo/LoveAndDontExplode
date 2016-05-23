using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class AccelerationVeloMotionControler : ControllerBase
{
	public MotionBase Motion;

	public Vector2 StartingVelocity;

	public MinMax XAcceleration;
	public MinMax YAcceleration;

	public MinMax XAccelerationVelocity;
	public MinMax YAccelerationVelocity;

	Vector2 velocity;
	float xa;
	float ya;
	float xav;
	float yav;

	TimeComponent Time { get { return GetComponent<TimeComponent>(); } }

	override protected void Start()
	{
		base.Start();
		velocity = StartingVelocity;
		xa = XAcceleration.GetRandom(ProbabilityDistributions.InversedGaussian);
		ya = YAcceleration.GetRandom(ProbabilityDistributions.InversedGaussian);
		xa = XAcceleration.GetRandom() * Mathf.Sign(xa);
		ya = YAcceleration.GetRandom() * Mathf.Sign(ya);

		transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
	}
	void FixedUpdate()
	{
		velocity += new Vector2(xa, ya) * Time.DeltaTime;
		xa += xav * Time.DeltaTime;
		ya += yav * Time.DeltaTime;

		Motion.Move(velocity.normalized);
		Motion.RotateTo(Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
	}
}