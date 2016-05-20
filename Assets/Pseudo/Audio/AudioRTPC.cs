using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;
using Pseudo.Pooling;

namespace Pseudo.Audio
{
	[Serializable]
	public class AudioRTPC : IPoolable, ICopyable<AudioRTPC>
	{
		public enum RTPCTypes
		{
			Volume,
			Pitch
		}

		public enum RTPCScope
		{
			Local,
			Global
		}

		static readonly Dictionary<string, AudioValue<float>> rtpcValues = new Dictionary<string, AudioValue<float>>();

		AudioValue<float> value;
		float lastValue;
		float lastRatio;

		public string Name;
		public RTPCTypes Type;
		public RTPCScope Scope;
		public MinMax Range = new MinMax(0f, 1f);
		[Clamp]
		public AnimationCurve Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		public float GetAdjustedValue()
		{
			float ratio = GetRatio();
			float value;

			if (ratio == lastRatio)
				value = lastValue;
			else
				value = Curve.Evaluate(ratio);

			lastRatio = ratio;
			lastValue = value;

			return value;
		}

		public void SetValue(float value)
		{
			this.value.Value = value;
		}

		float GetRatio()
		{
			return Mathf.Clamp01((value.Value - Range.Min) / (Range.Max - Range.Max));
		}

		public virtual void OnCreate()
		{
			if (Scope == RTPCScope.Local)
				//value = TypePoolManager.Create<AudioValue<float>>();
				value = new AudioValue<float>();
			else
				value = GetGlobalRTPCValue(Name);

			lastValue = Curve.Evaluate(GetRatio());
		}

		public virtual void OnRecycle()
		{
			//if (Scope == RTPCScope.Local)
			//	TypePoolManager.Recycle(ref value);
		}

		public void Copy(AudioRTPC source)
		{
			value = source.value;
			lastValue = source.lastValue;
			lastRatio = source.lastRatio;
			Name = source.Name;
			Type = source.Type;
			Scope = source.Scope;
			Range = source.Range;
			Curve = source.Curve;
		}

		public void CopyTo(AudioRTPC target)
		{
			target.Copy(this);
		}

		public static AudioValue<float> GetGlobalRTPCValue(string name)
		{
			AudioValue<float> value;

			if (!rtpcValues.TryGetValue(name, out value))
			{
				//value = TypePoolManager.Create<AudioValue<float>>();
				value = new AudioValue<float>();
				rtpcValues[name] = value;
			}

			return value;
		}

		public static void SetGlobalRTPCValue(string name, float value)
		{
			GetGlobalRTPCValue(name).Value = value;
		}
	}
}