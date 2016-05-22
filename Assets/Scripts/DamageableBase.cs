using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class DamageableBase : PMonoBehaviour
{
	[Header("'Damage' is the minimum damage to actually cause damage.")]
	public DamageInfo DamageableBy;

	public abstract bool Alive { get; }

	public virtual bool CanBeDamagedBy(DamageInfo info)
	{
		return
			DamageableBy.Damage <= info.Damage &&
			(DamageableBy.Sources & info.Sources) == info.Sources &&
			(DamageableBy.Types & info.Types) == info.Types;
	}

	public abstract bool Damage(DamageInfo info);
}
