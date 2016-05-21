using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class FormationComponent : PMonoBehaviour
{
	public abstract Vector3 GetFormationPosition(int index, int total);

}