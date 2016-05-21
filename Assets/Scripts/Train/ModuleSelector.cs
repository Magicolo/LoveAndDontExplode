using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Oscillation;
using Pseudo.Oscillation.Internal;

public class ModuleSelector : SelectorBase
{
	public float FadeSpeed = 5f;
	public Color IdleColor = Color.white;
	public Color InUseColor = Color.white;
	public OscillationSettings Oscillation;
	public SpriteRenderer Renderer;
	public TimeComponent Time;

	void Update()
	{
		var currentColor = Renderer.color;
		var targetColor = InUse ? InUseColor : IdleColor;

		if (Showing)
			Renderer.color = Color.Lerp(currentColor, targetColor.SetValues(1f, Channels.A), Time.DeltaTime * FadeSpeed);
		else
			Renderer.color = Color.Lerp(currentColor, targetColor.SetValues(0f, Channels.A), Time.DeltaTime * FadeSpeed);

		if (InUse)
			transform.localScale = Vector2.one;
		else if (currentColor.a > 0.1f)
		{
			var value = OscillationUtility.Oscillate(Oscillation, Time.Time);
			transform.localScale = new Vector2(value, value);
		}
	}
}