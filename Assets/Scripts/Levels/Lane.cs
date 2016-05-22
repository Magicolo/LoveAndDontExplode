using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Lane : PMonoBehaviour
{
	public float AnimationSpeed = 5f;
	public Renderer Renderer;
	public TimeComponent Time;

	void Update()
	{
		var offset = Renderer.material.mainTextureOffset;
		offset.x -= AnimationSpeed * Time.DeltaTime;
		offset.x = offset.x.Wrap(0f, 1f);
		Renderer.material.mainTextureOffset = offset;
	}
}
