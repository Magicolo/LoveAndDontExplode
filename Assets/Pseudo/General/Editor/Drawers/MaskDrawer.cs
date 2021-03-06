﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pseudo.Editor.Internal
{
	[CustomPropertyDrawer(typeof(MaskAttribute))]
	public class MaskDrawer : CustomAttributePropertyDrawerBase
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			drawPrefixLabel = false;

			Begin(position, property, label);

			EditorGUI.BeginChangeCheck();

			var enumValues = Enum.GetValues(fieldInfo.FieldType);
			int value = (int)enumValues.GetValue(property.GetValue<int>());
			var options = GetDisplayOptions();
			value = EditorGUI.MaskField(currentPosition, label, value, options);

			if (EditorGUI.EndChangeCheck())
			{
				object enumValue = value == -1 ? Array.IndexOf(enumValues, Enum.ToObject(fieldInfo.FieldType, SumOptions(options))) : Array.IndexOf(enumValues, Enum.ToObject(fieldInfo.FieldType, value));
				property.SetValue(enumValue);
			}

			End();
		}

		string[] GetDisplayOptions()
		{
			int filter = ((MaskAttribute)attribute).Filter;
			var values = Enum.GetValues(fieldInfo.FieldType);
			var names = Enum.GetNames(fieldInfo.FieldType);
			var options = new List<string>();

			for (int i = 0; i < values.Length; i++)
			{
				int value = (int)values.GetValue(i);

				if (((filter & value) != 0) && (value != 0) && ((value & (value - 1)) == 0))
					options.Add(names[i]);
			}

			return options.ToArray();
		}

		int SumOptions(string[] options)
		{
			int sum = 0;

			foreach (string option in options)
				sum += (int)Enum.Parse(fieldInfo.FieldType, option);

			return sum;
		}
	}
}