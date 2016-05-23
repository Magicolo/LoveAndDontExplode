using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ColorizeOnDamaged : PMonoBehaviour
{
	public Color NormalColor = Color.white;
	public Color DamagedColor = Color.red;
	public float FadeSpeed = 5f;
	public SpriteRenderer Renderer;
	public TimeComponent Time;

	void Update()
	{
		Renderer.color = Renderer.color.Lerp(NormalColor, Time.DeltaTime * FadeSpeed, Channels.RGB);
	}

	void OnDamaged()
	{
		Renderer.color = DamagedColor;
	}
}
