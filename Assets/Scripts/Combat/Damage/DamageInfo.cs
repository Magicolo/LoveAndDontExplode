using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[Flags]
public enum DamageSources : byte
{
	Player = 1 << 0,
	Enemy = 1 << 1
}

[Flags]
public enum DamageTypes : byte
{
	Bullet = 1 << 0,
	Lazer = 1 << 1,
	Leap = 1 << 2
}

[Serializable]
public struct DamageInfo
{
	public float Damage;
	[EnumFlags]
	public DamageSources Sources;
	[EnumFlags]
	public DamageTypes Types;
}
