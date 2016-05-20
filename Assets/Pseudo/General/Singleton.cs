﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pseudo;

namespace Pseudo
{
	[DisallowMultipleComponent]
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		protected static T instance;
		public static T Instance
		{
			get { return instance; }
		}

		public static T Find()
		{
			if (instance == null)
				instance = FindObjectOfType<T>();

			return instance;
		}

		protected virtual void Awake()
		{
			if (instance == null)
				instance = this as T;
		}
	}
}