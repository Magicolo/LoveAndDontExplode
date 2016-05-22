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

	public float AngleOffset;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		float arc = Spread * Mathf.Deg2Rad;
		float a = (AngleOffset - Spread / 2) * Mathf.Deg2Rad + index * arc / total;
		float x = (float)(Radius * Math.Cos(a));
		float y = (float)(Radius * Math.Sin(a));

		return new Vector3(x, y, 0);
	}
}