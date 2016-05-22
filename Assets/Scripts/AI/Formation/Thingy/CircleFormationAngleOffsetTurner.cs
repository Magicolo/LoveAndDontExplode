using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class CircleFormationAngleOffsetTurner : MonoBehaviour
{

	public float rate;
	void Update()
	{
		GetComponent<CircleFormation>().AngleOffset += rate * Time.deltaTime;
	}

}