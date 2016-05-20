using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class Motion : MotionBase
{
	Rigidbody2D body;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	public override void Move(Vector2 motion)
	{
		body.AddForce(motion, ForceMode2D.Impulse);
	}
}