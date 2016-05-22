﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class DamagerBase : PMonoBehaviour
{
	public DamageInfo DamageToCause;

	public virtual bool Damage(DamageableBase damageable, DamageInfo info)
	{
		return damageable.Damage(info);
	}
}
