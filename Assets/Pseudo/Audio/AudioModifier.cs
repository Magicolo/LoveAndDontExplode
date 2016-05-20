using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;
using Pseudo.Pooling;

namespace Pseudo.Audio.Internal
{
	public class AudioModifier : IPoolable, ICopyable<AudioModifier>
	{
		float initialValue = 1f;
		float fadeModifier = 1f;
		float rampModifier = 1f;
		float parentModifier = 1f;
		float randomModifier = 1f;
		float rtpcModifier = 1f;

		public virtual float Value { get { return initialValue * rampModifier * parentModifier * randomModifier * fadeModifier * rtpcModifier; } }
		public float InitialValue { get { return initialValue; } set { if (initialValue != value) { initialValue = value; RaiseValueChangedEvent(); }; } }
		public float FadeModifier { get { return fadeModifier; } set { if (fadeModifier != value) { fadeModifier = value; RaiseValueChangedEvent(); }; } }
		public float RampModifier { get { return rampModifier; } set { if (rampModifier != value) { rampModifier = value; RaiseValueChangedEvent(); }; } }
		public float ParentModifier { get { return parentModifier; } set { if (parentModifier != value) { parentModifier = value; RaiseValueChangedEvent(); }; } }
		public float RandomModifier { get { return randomModifier; } set { if (randomModifier != value) { randomModifier = value; RaiseValueChangedEvent(); }; } }
		public float RTPCModifier { get { return rtpcModifier; } set { if (rtpcModifier != value) { rtpcModifier = value; RaiseValueChangedEvent(); }; } }

		public event Action<AudioModifier> OnValueChanged;

		public void SimulateChange()
		{
			RaiseValueChangedEvent();
		}

		protected virtual void RaiseValueChangedEvent()
		{
			if (OnValueChanged != null)
				OnValueChanged(this);
		}

		public virtual void OnCreate() { }

		public virtual void OnRecycle()
		{
			OnValueChanged = null;
		}

		public void Copy(AudioModifier source)
		{
			initialValue = source.initialValue;
			fadeModifier = source.fadeModifier;
			rampModifier = source.rampModifier;
			parentModifier = source.parentModifier;
			randomModifier = source.randomModifier;
			rtpcModifier = source.rtpcModifier;
			OnValueChanged = source.OnValueChanged;
		}

		public void CopyTo(AudioModifier target)
		{
			target.Copy(this);
		}
	}
}