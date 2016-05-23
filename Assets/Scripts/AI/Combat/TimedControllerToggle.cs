using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TimedControllerToggle : MonoBehaviour
{
	public ControllerBase Controller;

	public float Delay;

	public TimeComponent Time;

	float t;
	void Start()
	{
		t = Time.Time + Delay;
	}

	void Update()
	{
		if (Time.Time >= t)
		{
			Controller.enabled = !Controller.enabled;
			enabled = false;
		}
	}
}