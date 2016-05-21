using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class CircleFormation : FormationComponent
{

	public float Radius = 1;

	[Min(0), Max(360)]
	public float Spread = 360;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		float arc = Spread * Mathf.Deg2Rad;
		float x = (float)(Radius * Math.Cos(index * arc / total));
		float y = (float)(Radius * Math.Sin(index * arc / total));

		return new Vector3(x, y, 0);
	}
}