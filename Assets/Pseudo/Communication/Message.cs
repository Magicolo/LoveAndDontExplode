﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Communication
{
	[Serializable]
	public struct Message : IEquatable<Message>, IEquatable<Enum>, ISerializationCallbackReceiver
	{
		public Enum Value
		{
			get
			{
				if (enumValue == null && Type != null)
					enumValue = (Enum)Enum.ToObject(Type, value);

				return enumValue;
			}
			set
			{
				if (value == null)
				{
					type = null;
					typeName = null;
					this.value = -1;
				}
				else
				{
					type = value.GetType();
					typeName = type.AssemblyQualifiedName;
					this.value = ((IConvertible)value).ToInt32(null);
				}

				enumValue = value;
			}
		}
		public Type Type
		{
			get
			{
				if (type == null && typeName != null)
					type = TypeUtility.GetType(typeName);

				return type;
			}
		}

		[SerializeField]
		int value;
		[SerializeField]
		string typeName;
		Type type;
		Enum enumValue;

		public Message(Enum value)
		{
			type = value.GetType();
			typeName = type.AssemblyQualifiedName;
			this.value = ((IConvertible)value).ToInt32(null);
			enumValue = value;
		}

		public bool Equals<T>(T obj)
		{
			if (Value is T)
				return PEqualityComparer<T>.Default.Equals(obj, (T)(object)Value);
			else
				return false;
		}

		public bool Equals(Enum other)
		{
			return enumValue == other;
		}

		public bool Equals(Message other)
		{
			return type == other.type && value == other.value;
		}

		public override bool Equals(object obj)
		{
			if (obj is Message)
				return Equals((Message)obj);
			else if (obj is Enum)
				return Equals((Enum)obj);
			else
				return false;
		}

		public override int GetHashCode()
		{
			if (type == null)
				return -1;
			else
				return type.GetHashCode() ^ value.GetHashCode();
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize() { }

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (typeName != null)
				type = TypeUtility.GetType(typeName);

			if (type != null)
				enumValue = (Enum)Enum.ToObject(type, value);
		}

		public static implicit operator Message(Enum enumValue)
		{
			return new Message(enumValue);
		}
	}
}
