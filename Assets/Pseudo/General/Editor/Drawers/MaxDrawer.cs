﻿using UnityEngine;
using UnityEditor;

namespace Pseudo.Editor.Internal
{
	[CustomPropertyDrawer(typeof(MaxAttribute))]
	public class MaxDrawer : CustomAttributePropertyDrawerBase
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			drawPrefixLabel = false;

			Begin(position, property, label);

			float max = ((MaxAttribute)attribute).max;

			EditorGUI.BeginChangeCheck();

			EditorGUI.PropertyField(currentPosition, property, label, true);

			if (EditorGUI.EndChangeCheck())
				property.Min(max);

			End();
		}
	}
}