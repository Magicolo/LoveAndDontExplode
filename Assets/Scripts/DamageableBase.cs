using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class DamageableBase : PMonoBehaviour
{
	public abstract bool Alive { get; }

	public abstract bool Damage(float damage);
}
