using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ToggleControllerAfterTime : MonoBehaviour
{
	public ControllerBase Controller;
	public TimeComponent Time;

	public float DeactivateTime;

	float t;

	void Start()
	{
		t = Time.Time + DeactivateTime;
	}

	void Update()
	{
		if (t > Time.Time)
		{
			Controller.enabled = !Controller.enabled;
		}
	}
}