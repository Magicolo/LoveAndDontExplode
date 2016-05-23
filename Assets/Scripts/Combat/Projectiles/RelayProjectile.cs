using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class RelayProjectile : ProjectileBase
{

	public ProjectileInfo[] Projectiles;


	public override GameObject[] Fire(Vector3 position, float angle)
	{
		List<GameObject> bullets = new List<GameObject>();
		for (int i = 0; i < Projectiles.Length; i++)
		{
			var p = Projectiles[i];
			var bs = p.Projectile.Fire(position + p.PositionOffset, angle + p.AngleOffset);
			bullets.AddRange(bs);
		}
		return bullets.ToArray();
	}
}

[Serializable]
public class ProjectileInfo
{
	public ProjectileBase Projectile;
	public Vector3 PositionOffset;
	public float AngleOffset;
}