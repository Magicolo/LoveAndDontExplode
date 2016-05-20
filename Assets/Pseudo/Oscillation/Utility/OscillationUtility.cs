﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using System.Reflection;
using UnityEngine.Assertions;

namespace Pseudo.Oscillation.Internal
{
	public static class OscillationUtility
	{
		static readonly Dictionary<PropertyInfo, IOscillator> propertyToOscillator = new Dictionary<PropertyInfo, IOscillator>();
		static readonly Func<OscillationSettings, float, float>[] waveFunctions =
		{
			Sine,
			Triangle,
			Square,
			InQuad,
			OutQuad,
			InOutQuad,
			OutInQuad,
			InCubic,
			OutCubic,
			InOutCubic,
			OutInCubic,
			SmoothStep,
			WhiteNoise,
			PerlinNoise,
		};

		public static bool IsValid(PropertyInfo property)
		{
			return property.CanRead && property.CanWrite &&
				(property.PropertyType == typeof(float) ||
				property.PropertyType == typeof(Vector2) ||
				property.PropertyType == typeof(Vector3) ||
				property.PropertyType == typeof(Vector4) ||
				property.PropertyType == typeof(Color));
		}

		public static float Oscillate(OscillationSettings settings, float time)
		{
			return GetWaveFunction(settings.WaveShape)(settings, time);
		}

		public static IOscillator GetOscillator(PropertyInfo property)
		{
			IOscillator oscillator;

			if (!propertyToOscillator.TryGetValue(property, out oscillator))
			{
				oscillator = CreateOscillator(property);
				propertyToOscillator[property] = oscillator;
			}

			return oscillator;
		}

		public static float Sine(OscillationSettings settings, float time)
		{
			return settings.Amplitude * Mathf.Sin(settings.Frequency * 2f * Mathf.PI * time + settings.Offset) + settings.Center;
		}

		public static float Sawtooth(OscillationSettings settings, float time)
		{
			return settings.Amplitude * PMath.Triangle(settings.Frequency * time + settings.Offset, 1f) + settings.Center;
		}

		public static float Triangle(OscillationSettings settings, float time)
		{
			return settings.Amplitude * PMath.Triangle(settings.Frequency * time + settings.Offset, settings.Ratio) + settings.Center;
		}

		public static float Square(OscillationSettings settings, float time)
		{
			return settings.Amplitude * PMath.Square(settings.Frequency * time + settings.Offset, settings.Ratio) + settings.Center;
		}

		public static float InQuad(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.InQuad(phase / settings.Ratio);
			else
				value = TweenUtility.InQuad(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float OutQuad(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.OutQuad(phase / settings.Ratio);
			else
				value = TweenUtility.OutQuad(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float InOutQuad(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.InOutQuad(phase / settings.Ratio);
			else
				value = TweenUtility.InOutQuad(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float OutInQuad(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.OutInQuad(phase / settings.Ratio);
			else
				value = TweenUtility.OutInQuad(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float InCubic(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.InCubic(phase / settings.Ratio);
			else
				value = TweenUtility.InCubic(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float OutCubic(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.OutCubic(phase / settings.Ratio);
			else
				value = TweenUtility.OutCubic(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float InOutCubic(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.InOutCubic(phase / settings.Ratio);
			else
				value = TweenUtility.InOutCubic(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float OutInCubic(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.OutInCubic(phase / settings.Ratio);
			else
				value = TweenUtility.OutInCubic(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float SmoothStep(OscillationSettings settings, float time)
		{
			float phase = (settings.Frequency * time + settings.Offset) % 1f;
			float value;

			if (phase < settings.Ratio)
				value = TweenUtility.SmoothStep(phase / settings.Ratio);
			else
				value = TweenUtility.SmoothStep(1f - (phase - settings.Ratio) / (1f - settings.Ratio));

			return settings.Amplitude * (value * 2f - 1f) + settings.Center;
		}

		public static float WhiteNoise(OscillationSettings settings, float time)
		{
			return settings.Amplitude * PRandom.Range(-1f, 1f) + settings.Center;
		}

		public static float PerlinNoise(OscillationSettings settings, float time)
		{
			return settings.Amplitude * (Mathf.Clamp01(Mathf.PerlinNoise(time * settings.Frequency, settings.Offset)) * 2f - 1f) + settings.Center;
		}

		public static Func<OscillationSettings, float, float> GetWaveFunction(WaveShapes waveShape)
		{
			return waveFunctions[(int)waveShape];
		}

		public static Vector2[] ToPoints(OscillationSettings settings, int definition)
		{
			var points = new Vector2[definition];

			for (int i = 0; i < definition; i++)
			{
				float ratio = (float)i / definition;
				points[i] = new Vector2(ratio, Oscillate(settings, ratio));
			}

			return points;
		}

		public static AnimationCurve ToCurve(OscillationSettings settings, int definition)
		{
			return new AnimationCurve(ToPoints(settings, definition).Convert(v => new Keyframe(v.x, v.y)));
		}

		static IOscillator CreateOscillator(PropertyInfo property)
		{
			Type oscillatorType = null;

			if (property.PropertyType == typeof(float))
				oscillatorType = typeof(FloatOscillator<>).MakeGenericType(property.DeclaringType);
			else if (property.PropertyType == typeof(Vector2))
				oscillatorType = typeof(Vector2Oscillator<>).MakeGenericType(property.DeclaringType);
			else if (property.PropertyType == typeof(Vector3))
				oscillatorType = typeof(Vector3Oscillator<>).MakeGenericType(property.DeclaringType);
			else if (property.PropertyType == typeof(Vector4))
				oscillatorType = typeof(Vector4Oscillator<>).MakeGenericType(property.DeclaringType);
			else if (property.PropertyType == typeof(Color))
				oscillatorType = typeof(ColorOscillator<>).MakeGenericType(property.DeclaringType);

			if (oscillatorType == null)
				return null;
			else
				return (IOscillator)Activator.CreateInstance(oscillatorType, property);
		}
	}
}
