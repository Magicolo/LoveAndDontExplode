using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FreezeMotion : PMonoBehaviour
{
	protected override void Start()
	{
		base.Start();

		var body = GetComponent<Rigidbody2D>();
		body.velocity = Vector2.zero;
		body.angularVelocity = 0f;
	}
}
