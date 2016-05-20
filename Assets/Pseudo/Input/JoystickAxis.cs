﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Internal;
using System;

namespace Pseudo.Input.Internal
{
	[System.Serializable]
	public class JoystickAxis : AxisBase
	{
		[SerializeField]
		protected Joysticks joystick;
		[SerializeField]
		protected JoystickAxes axis;
		[SerializeField, Min]
		protected float threshold;
		protected string axisName;

		public Joysticks Joystick
		{
			get { return joystick; }
			set
			{
				joystick = value;
				axisName = InputUtility.JoystickInputToAxis(joystick, axis);
			}
		}
		public JoystickAxes Axis
		{
			get { return axis; }
			set
			{
				axis = value;
				axisName = InputUtility.JoystickInputToAxis(joystick, axis);
			}
		}
		public override float Threshold
		{
			get { return threshold; }
			set { threshold = value; }
		}
		protected override string AxisName
		{
			get
			{
				if (string.IsNullOrEmpty(axisName))
					axisName = InputUtility.JoystickInputToAxis(joystick, axis);

				return axisName;
			}
		}

		public JoystickAxis(Joysticks joystick, JoystickAxes axis, float threshold)
		{
			this.joystick = joystick;
			this.axis = axis;
			this.axisName = InputUtility.JoystickInputToAxis(joystick, axis);
			this.threshold = threshold;
		}

		public JoystickAxis(string axisName, float threshold)
		{
			this.axisName = axisName;
			this.joystick = InputUtility.AxisToJoystick(axisName);
			this.axis = InputUtility.AxisToJoystickAxis(axisName);
			this.threshold = threshold;
		}
	}
}