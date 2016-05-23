using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ZoneFormation : FormationBase
{

	public Zone2DBase Zone;

	public override Vector3 GetFormationPosition(int index, int total)
	{
		return Zone.GetRandomLocalPoint();
	}
}