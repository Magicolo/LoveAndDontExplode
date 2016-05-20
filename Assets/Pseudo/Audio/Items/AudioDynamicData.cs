using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;
using UnityEngine.Audio;
using Pseudo.Internal;
using Pseudo.Audio.Internal;

namespace Pseudo.Audio
{
	public class AudioDynamicData : ICopyable
	{
		public enum PlayModes
		{
			Now,
			After
		}

		public PlayModes PlayMode = PlayModes.After;
		public double Delay;
		public Action<IAudioItem> OnInitialize;

		public void Copy(object reference)
		{
			var castedReference = (AudioDynamicData)reference;
			PlayMode = castedReference.PlayMode;
			Delay = castedReference.Delay;
			OnInitialize = castedReference.OnInitialize;
		}
	}
}