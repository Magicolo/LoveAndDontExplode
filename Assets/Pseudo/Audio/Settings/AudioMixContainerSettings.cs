using UnityEngine;
using System.Collections;
using System;
using Pseudo;
using System.Collections.Generic;

namespace Pseudo.Audio.Internal
{
	/// <summary>
	/// Container that will play all its sources after their delay.
	/// </summary>
	public class AudioMixContainerSettings : AudioContainerSettings
	{
		[Min]
		public List<double> Delays = new List<double>();

		public override AudioTypes Type { get { return AudioTypes.MixContainer; } }
	}
}