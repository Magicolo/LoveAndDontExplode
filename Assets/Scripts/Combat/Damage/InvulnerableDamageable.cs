using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class InvulnerableDamageable : DamageableBase
{
	public override bool Alive
	{
		get
		{
			return true;
		}
	}

	public override bool Damage(DamageInfo info)
	{
		return true;
	}
}