﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Internal;

namespace Pseudo
{
	public abstract class PComparer<T> : IComparer<T>
	{
		public static IComparer<T> Default
		{
			get
			{
				if (defaultComparer == null)
					defaultComparer = CreateComparer();

				return defaultComparer;
			}
		}

		static IComparer<T> defaultComparer;

		static IComparer<T> CreateComparer()
		{
			if (typeof(T).IsEnum)
			{
				var type = Enum.GetUnderlyingType(typeof(T));

				if (type == typeof(byte))
					return new EnumComparer<T, byte>();
				else if (type == typeof(sbyte))
					return new EnumComparer<T, sbyte>();
				else if (type == typeof(ushort))
					return new EnumComparer<T, ushort>();
				else if (type == typeof(short))
					return new EnumComparer<T, short>();
				else if (type == typeof(uint))
					return new EnumComparer<T, uint>();
				else if (type == typeof(int))
					return new EnumComparer<T, int>();
				else if (type == typeof(ulong))
					return new EnumComparer<T, ulong>();
				else if (type == typeof(long))
					return new EnumComparer<T, long>();
			}

			return Comparer<T>.Default;
		}

		public abstract int Compare(T x, T y);
	}
}
