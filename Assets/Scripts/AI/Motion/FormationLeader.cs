using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FormationLeader : MonoBehaviour
{

	public FormationBase Formation;
	public int FormationCount;

	public Vector3 GetMyPosition(int formatId)
	{
		return Formation.GetFormationPosition(formatId, FormationCount);
	}
}