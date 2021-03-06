﻿using System;
using UnityEngine;

namespace Pseudo.Editor.Internal
{
	public abstract class CustomAttributeBase : PropertyAttribute
	{
		public string PrefixLabel = "";
		public bool NoPrefixLabel;
		public bool NoFieldLabel;
		public bool NoIndex;
		public bool DisableOnPlay;
		public bool DisableOnStop;
		public string DisableBool;
		public int Indent;
		public bool BeforeSeparator;
		public bool AfterSeparator;
	}
}
