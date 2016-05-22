using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class RelayProjectile : ProjectileBase
{

	public ProjectileInfo[] Projectiles;


	public override void Fire(Vector3 position, float angle)
	{
		for (int i = 0; i < Projectiles.Length; i++)
		{
			var p = Projectiles[i];
			p.Projectile.Fire(position + p.PositionOffset, angle + p.AngleOffset);
		}
	}
}

[Serializable]
public class ProjectileInfo
{
	public ProjectileBase Projectile;
	public Vector3 PositionOffset;
	public float AngleOffset;
}