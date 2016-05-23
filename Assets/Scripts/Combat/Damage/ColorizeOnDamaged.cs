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
	public SpriteRenderer Renderer;
	public TimeComponent Time;

	void Update()
	{
		Renderer.FadeTowards(NormalColor, Time.DeltaTime);
	}

	void OnDamaged()
	{
		Renderer.color = DamagedColor;
	}
}
