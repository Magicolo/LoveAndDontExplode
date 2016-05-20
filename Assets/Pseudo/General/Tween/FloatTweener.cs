using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;

namespace Pseudo
{
	public class FloatTweener : ICopyable<FloatTweener>
	{
		public enum TweenStates
		{
			Waiting,
			Playing,
			Stopped
		}

		float start;
		float end;
		float time;
		float delay;
		Action<float> setValue;
		Func<float, float> easeFunction;
		Func<float> getDeltaTime;
		Action startCallback;
		Action endCallback;
		TweenStates state = TweenStates.Stopped;
		float value;
		float completion;
		float counter;

		public TweenStates State { get { return state; } }
		public float Value { get { return value; } }
		public float Completion { get { return completion; } }

		public void Update()
		{
			switch (state)
			{
				case TweenStates.Waiting:
					delay -= getDeltaTime();

					if (delay <= 0f)
					{
						SetState(TweenStates.Playing);
						Update();
					}
					break;
				case TweenStates.Playing:
					// Must be before to ensure at least one frame of ramping.
					if (counter >= time)
					{
						SetState(TweenStates.Stopped);
						return;
					}

					completion = Mathf.Clamp01(counter / time);
					value = (end - start) * easeFunction(completion) + start;
					setValue(value);
					counter += getDeltaTime();
					break;
			}
		}

		public void Stop()
		{
			if (state == TweenStates.Stopped)
				return;

			SetState(TweenStates.Stopped);
		}

		public void Ramp(float start, float end, float time, Action<float> setValue, TweenUtility.Ease ease = TweenUtility.Ease.Linear, Func<float> getDeltaTime = null, float delay = 0f, Action startCallback = null, Action endCallback = null)
		{
			this.start = start;
			this.end = end;
			this.time = time;
			this.setValue = setValue ?? TweenUtility.EmptyFloatAction;
			this.easeFunction = TweenUtility.GetEaseFunction(ease);
			this.getDeltaTime = getDeltaTime ?? (ApplicationUtility.IsPlaying ? TweenUtility.DefaultGetDeltaTime : TweenUtility.DefaultEditorGetDeltaTime);
			this.delay = delay;
			this.startCallback = startCallback ?? TweenUtility.EmptyAction;
			this.endCallback = endCallback ?? TweenUtility.EmptyAction;

			SetState(TweenStates.Waiting);
			Update();
		}

		void SetState(TweenStates state)
		{
			this.state = state;

			switch (this.state)
			{
				case TweenStates.Waiting:
					completion = 0f;
					counter = 0f;
					break;
				case TweenStates.Playing:
					value = start;
					startCallback();
					break;
				case TweenStates.Stopped:
					completion = 1f;
					counter = time;
					value = end;
					endCallback();
					break;
			}
		}

		public void Copy(FloatTweener reference)
		{
			start = reference.start;
			end = reference.end;
			time = reference.time;
			setValue = reference.setValue;
			easeFunction = reference.easeFunction;
			getDeltaTime = reference.getDeltaTime;
			delay = reference.delay;
			startCallback = reference.startCallback;
			endCallback = reference.endCallback;
			state = reference.state;
			value = reference.value;
			completion = reference.completion;
			counter = reference.counter;
		}

		public void CopyTo(FloatTweener instance)
		{
			instance.Copy(this);
		}
	}
}