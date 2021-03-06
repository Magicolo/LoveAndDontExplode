﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Editor.Internal
{
	[CustomPropertyDrawer(typeof(PopupAttribute))]
	public class PopupDrawer : CustomAttributePropertyDrawerBase
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			drawPrefixLabel = false;

			Begin(position, property, label);

			var arrayName = ((PopupAttribute)attribute).arrayName;
			var onChangeCallback = ((PopupAttribute)attribute).onChangeCallback;
			var array = property.serializedObject.FindProperty(arrayName);
			int selectedIndex = 0;

			var displayedOptions = new List<string>();
			if (array != null && property.GetValue() != null)
			{
				for (int i = 0; i < array.arraySize; i++)
				{
					var value = array.GetArrayElementAtIndex(i).GetValue();

					if (property.GetValue().Equals(value))
						selectedIndex = i;

					if (value != null)
					{
						if (value as Object != null)
							displayedOptions.Add(string.Format("{0} [{1}]", value.GetType().Name, i));
						else
							displayedOptions.Add(string.Format("{0}", value));
					}
					else
						displayedOptions.Add(" ");
				}
			}

			EditorGUI.BeginChangeCheck();
			selectedIndex = Mathf.Clamp(EditorGUI.Popup(currentPosition, label, selectedIndex, displayedOptions.ToGUIContents()), 0, array.arraySize - 1);

			if (array != null && array.arraySize != 0 && array.arraySize > selectedIndex)
				property.SetValue(array.GetArrayElementAtIndex(selectedIndex).GetValue());

			if (EditorGUI.EndChangeCheck())
			{
				if (!string.IsNullOrEmpty(onChangeCallback)) ((MonoBehaviour)property.serializedObject.targetObject).Invoke(onChangeCallback, 0);
			}

			End();
		}
	}
}
