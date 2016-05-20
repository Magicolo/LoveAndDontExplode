﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Pooling.Internal
{
	public class Storage<T> : IStorage<T> where T : class
	{
		public int Count
		{
			get { return instances.Count; }
		}
		public int Capacity
		{
			get { return capacity; }
			set
			{
				capacity = value;
				Trim(capacity);
			}
		}

		readonly IFactory<T> factory;
		readonly Queue<T> instances = new Queue<T>();
		readonly HashSet<T> hashedInstances = new HashSet<T>();
		int capacity = 1024;

		public Storage(IFactory<T> factory)
		{
			this.factory = factory;
		}

		public T Take()
		{
			var instance = instances.Dequeue();
			hashedInstances.Remove(instance);

			return instance;
		}

		public bool Put(T instance)
		{
			if (instance != null && Count < Capacity && hashedInstances.Add(instance))
			{
				instances.Enqueue(instance);
				return true;
			}
			else
				return false;
		}

		public void Fill(int count)
		{
			while (Count < count && Put(factory.Create())) { }
		}

		public void Trim(int count)
		{
			while (Count > count && Take() != null) { }
		}

		public bool Contains(T instance)
		{
			return hashedInstances.Contains(instance);
		}

		public void Clear()
		{
			instances.Clear();
			hashedInstances.Clear();
		}

		object IStorage.Take()
		{
			return Take();
		}

		bool IStorage.Put(object instance)
		{
			return Put(instance as T);
		}

		bool IStorage.Contains(object instance)
		{
			return Contains(instance as T);
		}
	}
}
