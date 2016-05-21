using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Testo : MonoBehaviour
{

	[Button("Robert", "Robert")]
	public bool btn;


	void Robert()
	{
		int preci = 10;
		double[] stats = new double[preci];
		int fucked = 0;
		double fsum = 0;
		for (int i = 0; i < 200; i++)
		{
			var d = PRandom.NextInversedGaussian();
			if (d <= 1 && d >= 0)
				stats[(int)(d * (preci - 1))]++;
			else
			{
				fucked++;
				fsum += d;
				Debug.Log(d);
			}

		}

		for (int i = 0; i < preci; i++)
		{
			Debug.Log(i + ": " + stats[i]);
		}

		Debug.Log("fuck : " + fucked);
		Debug.Log("fsum : " + fsum);
	}
}