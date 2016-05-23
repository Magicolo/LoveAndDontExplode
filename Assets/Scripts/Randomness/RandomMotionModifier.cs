using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class RandomMotionModifier : MonoBehaviour
{

	public Motion Motion;

	public MinMax MotionSpeedModifier;
	public MinMax MotionRotationModifier;
	public ProbabilityDistributions ProbabilityDistribution = ProbabilityDistributions.Uniform;


	void Start()
	{
		Motion.MoveSpeed *= MotionSpeedModifier.GetRandom(ProbabilityDistribution);
		Motion.RotateSpeed *= MotionRotationModifier.GetRandom(ProbabilityDistribution);
	}
}