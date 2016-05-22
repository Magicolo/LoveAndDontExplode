using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class DamagerBase : PMonoBehaviour
{
	public virtual bool Damage(Damageable damageable, float damageAmount)
	{
		return damageable.Damage(damageAmount);
	}
}
