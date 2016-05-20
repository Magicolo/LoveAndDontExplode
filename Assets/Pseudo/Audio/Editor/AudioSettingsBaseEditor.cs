﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;
using UnityEditor;
using Pseudo.Editor.Internal;
using Pseudo.Pooling;

namespace Pseudo.Audio.Internal
{
	public abstract class AudioSettingsBaseEditor : PEditor
	{
		UnityEditorInternal.ReorderableList list;
		PropertyDrawer drawer;
		AudioSettingsBase settings;

		public override void OnEnable()
		{
			base.OnEnable();

			settings = (AudioSettingsBase)target;
		}

		public override void End(bool space = true)
		{
			base.End(space);

			//if (GUI.changed)
			//	PrefabPoolManager.ResetPool(settings);
		}

		public void ShowType()
		{
			var style = new GUIStyle("boldLabel");
			style.alignment = TextAnchor.MiddleCenter;
			EditorGUILayout.LabelField(settings.Type.ToString().ToUpper(), style);
		}

		public void ShowGeneral()
		{
			Separator();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("Loop"));

			ShowFades();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("VolumeScale"));
			ShowPitchScale();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomVolume"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomPitch"));

			Separator();
		}

		public void ShowRTPCs()
		{
			ArrayFoldout(serializedObject.FindProperty("RTPCs"), "RTPCs".ToGUIContent(), disableOnPlay: false);
		}

		public void ShowOptions()
		{
			ArrayFoldout(serializedObject.FindProperty("Options"), disableOnPlay: false, reorderCallback: (p, s, t) => Repaint());
		}

		void ShowPitchScale()
		{
			EditorGUIUtility.fieldWidth -= 20f;

			EditorGUILayout.BeginHorizontal();

			var pitchScaleModeProperty = serializedObject.FindProperty("PitchScaleMode");
			var pitchScaleMode = pitchScaleModeProperty.GetValue<AudioSettingsBase.PitchScaleModes>();
			var pitchScaleProperty = serializedObject.FindProperty("PitchScale");

			if (pitchScaleMode == AudioSettingsBase.PitchScaleModes.Ratio)
				EditorGUILayout.PropertyField(pitchScaleProperty);
			else
			{
				float pitchScale = pitchScaleProperty.GetValue<float>();
				int selectedValue = Mathf.RoundToInt(Mathf.Log(pitchScale, 2f) * 12f);

				EditorGUI.BeginChangeCheck();

				selectedValue = EditorGUILayout.IntSlider("Pitch Scale", selectedValue, -24, 24);

				if (EditorGUI.EndChangeCheck())
				{
					pitchScale = Mathf.Pow(2f, selectedValue / 12f);

					for (int i = 0; i < targets.Length; i++)
					{
						var settings = (AudioSettingsBase)targets[i];
						settings.PitchScale = pitchScale;
					}

					serializedObject.Update();
				}
			}

			var style = new GUIStyle("button") { clipping = TextClipping.Overflow };

			EditorGUI.BeginChangeCheck();

			pitchScaleMode = GUILayout.Toggle(pitchScaleMode == AudioSettingsBase.PitchScaleModes.Ratio, "R", style, GUILayout.Width(16f), GUILayout.Height(14f)) ? AudioSettingsBase.PitchScaleModes.Ratio : AudioSettingsBase.PitchScaleModes.Semitone;

			if (EditorGUI.EndChangeCheck())
			{
				for (int i = 0; i < targets.Length; i++)
				{
					var settings = (AudioSettingsBase)targets[i];
					settings.PitchScaleMode = pitchScaleMode;
				}

				serializedObject.Update();
			}

			EditorGUILayout.EndHorizontal();
			EditorGUIUtility.fieldWidth += 20f;
		}

		void ShowFades()
		{
			var fadeInProperty = serializedObject.FindProperty("FadeIn");
			var fadeOutProperty = serializedObject.FindProperty("FadeOut");
			var fadeInEaseProperty = serializedObject.FindProperty("FadeInEase");
			var fadeOutEaseProperty = serializedObject.FindProperty("FadeOutEase");

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.PropertyField(fadeInProperty);

			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
				fadeOutProperty.Clamp(0f, GetSettingsLength(settings) - fadeInProperty.GetValue<float>());
			}

			ShowFadeEase(fadeInEaseProperty);

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.PropertyField(fadeOutProperty);

			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
				fadeInProperty.Clamp(0f, GetSettingsLength(settings) - fadeOutProperty.GetValue<float>());
			}

			ShowFadeEase(fadeOutEaseProperty);

			EditorGUILayout.EndHorizontal();
			if (EditorGUI.EndChangeCheck())
				ClampFades();
		}

		void ShowFadeEase(SerializedProperty easeProperty)
		{
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			EditorGUILayout.PropertyField(easeProperty, GUIContent.none, GUILayout.Width(Screen.width * 0.25f));
			EditorGUI.indentLevel = indent;
		}

		public virtual void ClampFades()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				var settings = (AudioSettingsBase)targets[i];
				settings.FadeIn = Mathf.Clamp(settings.FadeIn, 0f, GetSettingsLength(settings));
				settings.FadeOut = Mathf.Clamp(settings.FadeOut, 0f, GetSettingsLength(settings));
			}

			serializedObject.Update();
		}

		public abstract float GetSettingsLength(AudioSettingsBase settings);
	}
}