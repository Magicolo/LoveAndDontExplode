﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;
using UnityEditor;
using Pseudo.Editor.Internal;

namespace Pseudo.Audio.Internal
{
	[CustomEditor(typeof(AudioSwitchContainerSettings)), CanEditMultipleObjects]
	public class AudioSwitchContainerSettingsEditor : AudioContainerSettingsEditor
	{
		SerializedProperty switchValues;

		public override void OnInspectorGUI()
		{
			Begin(false);

			switchValues = serializedObject.FindProperty("SwitchValues");

			EditorGUILayout.PropertyField(serializedObject.FindProperty("SwitchName"));

			base.OnInspectorGUI();

			End(false);
		}

		public override void ShowSource(SerializedProperty arrayProperty, int index, SerializedProperty sourceProperty)
		{
			base.ShowSource(arrayProperty, index, sourceProperty);

			switchValues.arraySize = arrayProperty.arraySize;

			if (sourceProperty.isExpanded)
			{
				EditorGUI.indentLevel++;

				EditorGUILayout.PropertyField(sourceSettingsProperty);
				EditorGUILayout.PropertyField(switchValues.GetArrayElementAtIndex(index), "Switch Value".ToGUIContent());
				ArrayFoldout(sourceProperty.FindPropertyRelative("Options"), disableOnPlay: false);

				EditorGUI.indentLevel--;
			}
		}

		public override void OnSourceDeleted(SerializedProperty arrayProperty, int index)
		{
			base.OnSourceDeleted(arrayProperty, index);

			DeleteFromArray(switchValues, index);
		}

		public override void OnSourceReordered(SerializedProperty arrayProperty, int sourceIndex, int targetIndex)
		{
			base.OnSourceReordered(arrayProperty, sourceIndex, targetIndex);

			ReorderArray(switchValues, sourceIndex, targetIndex);
		}
	}
}