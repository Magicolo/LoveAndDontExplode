﻿using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;
using Pseudo.Audio.Internal;
using Pseudo.Audio;

namespace Pseudo.Editor.Internal
{
	public static class AudioCustomMenus
	{
		[MenuItem("Pseudo/Create/Audio Settings/Source", validate = true, priority = 9)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Source", validate = true, priority = 9)]
		static bool CreateAudioSourceSettingsValid()
		{
			return Array.Exists(Selection.objects, obj => obj is AudioClip);
		}

		[MenuItem("Pseudo/Create/Audio Settings/Source", priority = 9)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Source", priority = 9)]
		static void CreateAudioSourceSettings()
		{
			var clips = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);

			for (int i = 0; i < clips.Length; i++)
			{
				var clip = (AudioClip)clips[i];
				var settings = CreateAudioContainerSettings<AudioSourceSettings>(clip.name, AssetDatabase.GetAssetPath(clip));

				settings.name = clip.name;
				settings.Clip = clip;
			}
		}

		[MenuItem("Pseudo/Create/Audio Settings/Container/Mix", priority = 10)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Container/Mix", priority = 10)]
		static void CreateAudioMixContainerSettings()
		{
			CreateAudioContainerSettings<AudioMixContainerSettings>("Mix Container");
		}

		[MenuItem("Pseudo/Create/Audio Settings/Container/Random", priority = 11)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Container/Random", priority = 11)]
		static void CreateAudioRandomContainerSettings()
		{
			CreateAudioContainerSettings<AudioRandomContainerSettings>("Random Container");
		}

		[MenuItem("Pseudo/Create/Audio Settings/Container/Enumerator", priority = 12)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Container/Enumerator", priority = 12)]
		static void CreateAudioEnumeratorContainerSettings()
		{
			CreateAudioContainerSettings<AudioEnumeratorContainerSettings>("Enumerator Container");
		}

		[MenuItem("Pseudo/Create/Audio Settings/Container/Switch", priority = 13)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Container/Switch", priority = 13)]
		static void CreateAudioSwitchContainerSettings()
		{
			CreateAudioContainerSettings<AudioSwitchContainerSettings>("Switch Container");
		}

		[MenuItem("Pseudo/Create/Audio Settings/Container/Sequence", priority = 14)]
		[MenuItem("Assets/Create/Pseudo/Audio Settings/Container/Sequence", priority = 14)]
		static void CreateAudioSequenceContainerSettings()
		{
			CreateAudioContainerSettings<AudioSequenceContainerSettings>("Sequence Container");
		}

		static T CreateAudioContainerSettings<T>(string name, string settingsPath = "") where T : AudioSettingsBase
		{
			T settings = ScriptableObject.CreateInstance<T>();
			string path = AssetDatabaseUtility.GenerateUniqueAssetPath(name, path: settingsPath);
			AssetDatabase.CreateAsset(settings, path);
			Selection.activeObject = settings;

			return settings;
		}
	}
}
