using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

[RequireComponent(typeof(TimeComponent))]
public class RandomXPointMotionControler : ControllerBase
{

	public MotionBase Motion;

	public MinMax XRange;
	public MinMax HoldPointTime;

	public bool goingRight;

	float targetX;
	float t;

	override protected void Start()
	{
		goingRight = !goingRight;
		changeNextTargerX();
	}

	private void changeNextTargerX()
	{
		targetX = cam.ViewportToWorldPoint(new Vector3(XRange.GetRandom(), 0, -cam.transform.position.z)).x;
		t = 0;
		goingRight = !goingRight;
	}

	void FixedUpdate()
	{

		if (t <= 0)
		{
			Motion.Move(new Vector3(1, 0, 0) * (goingRight ? 1 : -1));
			if (goingRight && transform.position.x >= targetX)
				t = GetComponent<TimeComponent>().Time + HoldPointTime.GetRandom();
			else if (!goingRight && transform.position.x <= targetX)
				t = GetComponent<TimeComponent>().Time + HoldPointTime.GetRandom();
		}
		else if (GetComponent<TimeComponent>().Time >= t)
		{
			changeNextTargerX();
		}

	}
}