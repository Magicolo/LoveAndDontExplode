using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class SinMotionControler : ControllerBase
{
	public MotionBase Motion;

	float t;
	public float Frequency = 1;
	public float Amplitude = 1;
	public float Offset;


	void FixedUpdate()
	{
		var y = (float)(Amplitude * Math.Sin(Frequency * t + Offset));
		var direction = new Vector2(1, y).Rotate(Motion.transform.rotation.eulerAngles.z);
		t += GetComponent<TimeComponent>().DeltaTime;

		if (direction != Vector2.zero)
		{
			Motion.Move(direction);
		}
	}
}