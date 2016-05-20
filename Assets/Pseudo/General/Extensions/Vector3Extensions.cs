﻿using UnityEngine;
using System.Collections;

namespace Pseudo
{
	public static class Vector3Extensions
	{
		const float epsilon = 0.0001F;

		public static Vector3 SetValues(this Vector3 vector, Vector3 values, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = values.x;

			if ((axes & Axes.Y) != 0)
				vector.y = values.y;

			if ((axes & Axes.Z) != 0)
				vector.z = values.z;

			return vector;
		}

		public static Vector3 SetValues(this Vector3 vector, Vector3 values)
		{
			return vector.SetValues(values, Axes.XYZW);
		}

		public static Vector3 SetValues(this Vector3 vector, float value, Axes axes)
		{
			return vector.SetValues(new Vector3(value, value, value), axes);
		}

		public static Vector3 SetValues(this Vector3 vector, float value)
		{
			return vector.SetValues(new Vector3(value, value, value), Axes.XYZ);
		}

		public static Vector3 Lerp(this Vector3 vector, Vector3 target, float deltaTime, Axes axes)
		{
			if ((axes & Axes.X) != 0 && Mathf.Abs(target.x - vector.x) > epsilon)
				vector.x = Mathf.Lerp(vector.x, target.x, deltaTime);

			if ((axes & Axes.Y) != 0 && Mathf.Abs(target.y - vector.y) > epsilon)
				vector.y = Mathf.Lerp(vector.y, target.y, deltaTime);

			if ((axes & Axes.Z) != 0 && Mathf.Abs(target.z - vector.z) > epsilon)
				vector.z = Mathf.Lerp(vector.z, target.z, deltaTime);

			return vector;
		}

		public static Vector3 Lerp(this Vector3 vector, float target, float deltaTime, Axes axes)
		{
			return vector.Lerp(new Vector3(target, target, target), deltaTime, axes);
		}

		public static Vector3 LerpLinear(this Vector3 vector, Vector3 target, float deltaTime, Axes axes)
		{
			var difference = target - vector;
			var direction = Vector3.zero.SetValues(difference, axes);
			float distance = direction.magnitude;

			var adjustedDirection = direction.normalized * deltaTime;

			if (adjustedDirection.magnitude < distance)
				vector += Vector3.zero.SetValues(adjustedDirection, axes);
			else
				vector = vector.SetValues(target, axes);

			return vector;
		}

		public static Vector3 LerpLinear(this Vector3 vector, Vector3 target, float deltaTime)
		{
			return vector.LerpLinear(target, deltaTime, Axes.XYZW);
		}

		public static Vector3 LerpAngles(this Vector3 vector, Vector3 targetAngles, float deltaTime, Axes axes)
		{
			if ((axes & Axes.X) != 0 && Mathf.Abs(targetAngles.x - vector.x) > epsilon)
				vector.x = Mathf.LerpAngle(vector.x, targetAngles.x, deltaTime);

			if ((axes & Axes.Y) != 0 && Mathf.Abs(targetAngles.y - vector.y) > epsilon)
				vector.y = Mathf.LerpAngle(vector.y, targetAngles.y, deltaTime);

			if ((axes & Axes.Z) != 0 && Mathf.Abs(targetAngles.z - vector.z) > epsilon)
				vector.z = Mathf.LerpAngle(vector.z, targetAngles.z, deltaTime);

			return vector;
		}

		public static Vector3 LerpAngles(this Vector3 vector, Vector3 targetAngles, float time)
		{
			return vector.LerpAngles(targetAngles, time, Axes.XYZW);
		}

		public static Vector3 LerpAnglesLinear(this Vector3 vector, Vector3 targetAngles, float time, Axes axes)
		{
			var difference = new Vector3(Mathf.DeltaAngle(vector.x, targetAngles.x), Mathf.DeltaAngle(vector.y, targetAngles.y), Mathf.DeltaAngle(vector.z, targetAngles.z));
			var direction = Vector3.zero.SetValues(difference, axes);
			float distance = direction.magnitude * Mathf.Rad2Deg;

			var adjustedDirection = direction.normalized * time;

			if (adjustedDirection.magnitude < distance)
				vector += Vector3.zero.SetValues(adjustedDirection, axes);
			else
				vector = vector.SetValues(targetAngles, axes);

			return vector;
		}

		public static Vector3 LerpAnglesLinear(this Vector3 vector, Vector3 targetAngles, float time)
		{
			return vector.LerpAnglesLinear(targetAngles, time, Axes.XYZW);
		}

		public static float Distance(this Vector3 vector, Vector3 target)
		{
			return Vector3.Distance(vector, target);
		}

		public static float Distance(this Vector3 vector, Vector3 target, Axes axes)
		{
			float distance = 0f;

			switch (axes)
			{
				case Axes.X:
					distance = Mathf.Abs(vector.x - target.x);
					break;
				case Axes.Y:
					distance = Mathf.Abs(vector.y - target.y);
					break;
				case Axes.Z:
					distance = Mathf.Abs(vector.z - target.z);
					break;
				case Axes.XY:
					distance = Vector2.Distance(vector, target);
					break;
				case Axes.XZ:
					distance = Vector2.Distance(new Vector2(vector.x, vector.z), new Vector2(target.x, target.z));
					break;
				case Axes.YZ:
					distance = Vector2.Distance(new Vector2(vector.y, vector.z), new Vector2(target.y, target.z));
					break;
				case Axes.XYZ:
					distance = Vector3.Distance(vector, target);
					break;
			}

			return distance;
		}

		public static Vector3 Oscillate(this Vector3 vector, Vector3 frequency, Vector3 amplitude, Vector3 center, Vector3 offset, float time, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = center.x + amplitude.x * Mathf.Sin(frequency.x * time + offset.x);

			if ((axes & Axes.Y) != 0)
				vector.y = center.y + amplitude.y * Mathf.Sin(frequency.y * time + offset.y);

			if ((axes & Axes.Z) != 0)
				vector.z = center.z + amplitude.z * Mathf.Sin(frequency.z * time + offset.z);

			return vector;
		}

		public static Vector3 Oscillate(this Vector3 vector, float frequency, float amplitude, float center, float offset, float time, Axes axes)
		{
			return vector.Oscillate(
				new Vector3(frequency, frequency, frequency),
				new Vector3(amplitude, amplitude, amplitude),
				new Vector3(center, center, center),
				new Vector3(offset, offset, offset),
				time, axes);
		}

		public static Vector3 Mult(this Vector3 vector, Vector3 values, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x *= values.x;

			if ((axes & Axes.Y) != 0)
				vector.y *= values.y;

			if ((axes & Axes.Z) != 0)
				vector.z *= values.z;

			return vector;
		}

		public static Vector3 Mult(this Vector3 vector, Vector3 values)
		{
			return vector.Mult(values, Axes.XYZW);
		}

		public static Vector3 Div(this Vector3 vector, Vector3 values, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x /= values.x;

			if ((axes & Axes.Y) != 0)
				vector.y /= values.y;

			if ((axes & Axes.Z) != 0)
				vector.z /= values.z;

			return vector;
		}

		public static Vector3 Div(this Vector3 vector, Vector3 values)
		{
			return vector.Div(values, Axes.XYZW);
		}

		public static Vector3 Pow(this Vector3 vector, float power, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = Mathf.Pow(vector.x, power);

			if ((axes & Axes.Y) != 0)
				vector.y = Mathf.Pow(vector.y, power);

			if ((axes & Axes.Z) != 0)
				vector.z = Mathf.Pow(vector.z, power);

			return vector;
		}

		public static Vector3 Pow(this Vector3 vector, float power)
		{
			return vector.Pow(power, Axes.XYZW);
		}

		public static Vector3 Round(this Vector3 vector, float step, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = vector.x.Round(step);

			if ((axes & Axes.Y) != 0)
				vector.y = vector.y.Round(step);

			if ((axes & Axes.Z) != 0)
				vector.z = vector.z.Round(step);

			return vector;
		}

		public static Vector3 Round(this Vector3 vector, float step)
		{
			return vector.Round(step, Axes.XYZW);
		}

		public static Vector3 Round(this Vector3 vector)
		{
			return vector.Round(1f, Axes.XYZW);
		}

		public static Vector3 Floor(this Vector3 vector, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = Mathf.Floor(vector.x);

			if ((axes & Axes.Y) != 0)
				vector.y = Mathf.Floor(vector.y);

			if ((axes & Axes.Z) != 0)
				vector.z = Mathf.Floor(vector.z);

			return vector;
		}

		public static Vector3 Floor(this Vector3 vector)
		{
			return vector.Floor(Axes.XYZW);
		}

		public static Vector3 Ceil(this Vector3 vector, Axes axes)
		{
			if ((axes & Axes.X) != 0)
				vector.x = Mathf.Ceil(vector.x);

			if ((axes & Axes.Y) != 0)
				vector.y = Mathf.Ceil(vector.y);

			if ((axes & Axes.Z) != 0)
				vector.z = Mathf.Ceil(vector.z);

			return vector;
		}

		public static Vector3 Ceil(this Vector3 vector)
		{
			return vector.Ceil(Axes.XYZW);
		}

		public static float Average(this Vector3 vector, Axes axes)
		{
			float average = 0f;
			int axisCount = 0;

			if ((axes & Axes.X) != 0)
			{
				average += vector.x;
				axisCount += 1;
			}

			if ((axes & Axes.Y) != 0)
			{
				average += vector.y;
				axisCount += 1;
			}

			if ((axes & Axes.Z) != 0)
			{
				average += vector.z;
				axisCount += 1;
			}

			return average / axisCount;
		}

		public static float Average(this Vector3 vector)
		{
			return vector.Average(Axes.XYZW);
		}

		public static Vector3 Rotate(this Vector3 vector, float angle)
		{
			return vector.Rotate(angle, Vector3.forward);
		}

		public static Vector3 Rotate(this Vector3 vector, float angle, Vector3 axis)
		{
			angle %= 360;

			return Quaternion.AngleAxis(-angle, axis) * vector;
		}

		public static Vector3 ClampMagnitude(this Vector3 vector, float min, float max)
		{
			float sqrMagniture = vector.sqrMagnitude;
			float sqrMin = min * min;
			float sqrMax = max * max;

			if (sqrMagniture < sqrMin)
				vector = vector.normalized * min;
			else if (sqrMagniture > sqrMax)
				vector = vector.normalized * max;

			return vector;
		}

		public static Vector3 ToPolar(this Vector3 vector)
		{
			Vector2 polar = vector.ToVector2().ToPolar();
			return new Vector3(polar.x, polar.y, vector.z);
		}

		public static Vector3 ToCartesian(this Vector3 vector)
		{
			Vector2 cartesian = vector.ToVector2().ToCartesian();
			return new Vector3(cartesian.x, cartesian.y, vector.z);
		}

		public static Vector2 ToVector2(this Vector3 vector)
		{
			return vector;
		}

		public static Vector4 ToVector4(this Vector3 vector)
		{
			return vector;
		}
	}
}
