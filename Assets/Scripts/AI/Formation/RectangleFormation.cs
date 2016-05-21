using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class RectangleFormation : FormationComponent
{

	[Min(0)]
	public float width;
	[Min(0)]
	public float height;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		float c = 2 * width + 2 * height;

		float cp = index * c / total;

		float x = 0;
		float y = 0;

		float hh = height / 2;
		float hw = width / 2;

		if (cp < height)
		{
			x = hw;
			y = -hh + cp;
		}
		else if (cp < width + height)
		{
			x = hw - (cp - height);
			y = hh;
		}
		else if (cp < width + 2 * height)
		{
			x = -hw;
			y = hh - (cp - width - height);
		}
		else
		{
			x = -hw + (cp - width - 2 * height);
			y = -hh;
		}

		return new Vector3(x, y, 0);
	}
}