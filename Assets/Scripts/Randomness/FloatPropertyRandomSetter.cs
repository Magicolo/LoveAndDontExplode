using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.References;

public class FloatPropertyRandomSetter : MonoBehaviour
{
	public FloatProperty Property;
	public MinMax Range;
	public ProbabilityDistributions ProbabilityDistribution;

	void Start()
	{
		if (Property.Target != null)
			Property.Value = Range.GetRandom(ProbabilityDistribution);
	}
}