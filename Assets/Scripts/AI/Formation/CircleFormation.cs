using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class CircleFormation : FormationComponent
{

	public float Radius = 1;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		float x = (float)(Radius * Math.Cos(index * 2 * Math.PI / total));
		float y = (float)(Radius * Math.Sin(index * 2 * Math.PI / total));

		return new Vector3(x, y, 0);
	}
}