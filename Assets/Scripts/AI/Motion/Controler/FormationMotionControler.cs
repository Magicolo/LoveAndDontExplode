using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class FormationMotionControler : ControllerBase
{
	public MotionBase Motion;
	public FormationLeader FormationLeader;
	public int formatId;

	void FixedUpdate()
	{
		var target = FormationLeader.transform.position + FormationLeader.GetMyPosition(formatId);
		var v = target - transform.position;
		Motion.Move(v.normalized);
	}
}