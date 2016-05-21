using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class BirdFlockFormation : FormationComponent
{

	[Min(0), Max(360)]
	public float SpreadAngle = 60;

	[Min(2)]
	public int NbRows = 2;

	[Min(0)]
	public float Spacing;

	public float AngleOffset;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		float rowIndex = (float)((index == 0) ? 0 : ((index - 1) % NbRows) / (NbRows - 1.0));
		float SpacingIndex = (index == 0) ? 0 : Mathf.Ceil(index / NbRows);


		float a = Mathf.Deg2Rad * (AngleOffset + 180 - SpreadAngle / 2 + rowIndex * SpreadAngle);
		float r = SpacingIndex * Spacing;
		float x = (float)(r * Math.Cos(a));
		float y = (float)(r * Math.Sin(a));

		return new Vector3(x, y, 0);
	}
}