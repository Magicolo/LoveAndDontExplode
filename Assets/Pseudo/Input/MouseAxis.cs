﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

namespace Pseudo.Input.Internal
{
	[Serializable]
	public class MouseAxis
	{
		[SerializeField]
		protected MouseAxes axis;
		[SerializeField, Min]
		protected float scale = 1f;
		[SerializeField, Min]
		protected float threshold;

		protected bool axisJustDown;
		protected bool axisJustUp;
		protected bool axisDown;

		public MouseAxes Axis
		{
			get { return axis; }
			set { axis = value; }
		}
		public float Scale
		{
			get { return scale; }
			set { scale = value; }
		}
		public float Threshold
		{
			get { return threshold; }
			set { threshold = value; }
		}

		public MouseAxis(MouseAxes axis, float threshold)
		{
			this.axis = axis;
			this.threshold = threshold;
		}

		public float GetValue(Vector2 relativeScreenPosition)
		{
			float value = 0f;

			switch (axis)
			{
				case MouseAxes.X:
					value = UnityEngine.Input.mousePosition.x - relativeScreenPosition.x;
					break;
				case MouseAxes.Y:
					value = UnityEngine.Input.mousePosition.y - relativeScreenPosition.y;
					break;
				case MouseAxes.WheelX:
					value = UnityEngine.Input.mouseScrollDelta.x;
					break;
				case MouseAxes.WheelY:
					value = UnityEngine.Input.mouseScrollDelta.y;
					break;
			}

			value = (Mathf.Abs(value) >= threshold ? value : 0f) * scale;
			axisJustDown = !axisDown && value != 0f;
			axisJustUp = axisDown && value == 0f;
			axisDown = value != 0f;

			return value;
		}

		public bool GetAxisDown(Vector2 relativeScreenPosition)
		{
			GetValue(relativeScreenPosition);

			return axisJustDown;
		}

		public bool GetAxisUp(Vector2 relativeScreenPosition)
		{
			GetValue(relativeScreenPosition);

			return axisJustUp;
		}

		public bool GetAxis(Vector2 relativeScreenPosition)
		{
			GetValue(relativeScreenPosition);

			return axisDown;
		}
	}
}
