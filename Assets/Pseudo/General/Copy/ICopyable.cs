﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pseudo
{
	public interface ICopyable<T>
	{
		void Copy(T reference);
		void CopyTo(T instance);
	}

	public interface ICopyable
	{
		void Copy(object reference);
	}
}
