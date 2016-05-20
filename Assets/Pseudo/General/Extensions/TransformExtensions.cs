﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Pseudo
{
	public static class TransformExtensions
	{
		#region Position
		public static void SetPosition(this Transform transform, Vector3 position, Axes axes = Axes.XYZ)
		{
			transform.position = transform.position.SetValues(position, axes);
		}

		public static void SetPosition(this Transform transform, float position, Axes axes = Axes.XYZ)
		{
			transform.SetPosition(new Vector3(position, position, position), axes);
		}

		public static void Translate(this Transform transform, Vector3 translation, Axes axes = Axes.XYZ)
		{
			transform.SetPosition(transform.position + translation, axes);
		}

		public static void Translate(this Transform transform, float translation, Axes axes = Axes.XYZ)
		{
			transform.Translate(new Vector3(translation, translation, translation), axes);
		}

		public static void TranslateTowards(this Transform transform, Vector3 targetPosition, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetPosition(transform.position.Lerp(targetPosition, deltaTime, axes), axes);
		}

		public static void TranslateTowards(this Transform transform, float targetPosition, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.TranslateTowards(new Vector3(targetPosition, targetPosition, targetPosition), deltaTime, axes);
		}

		public static void SetLocalPosition(this Transform transform, Vector3 position, Axes axes = Axes.XYZ)
		{
			transform.localPosition = transform.localPosition.SetValues(position, axes);
		}

		public static void SetLocalPosition(this Transform transform, float position, Axes axes = Axes.XYZ)
		{
			transform.SetLocalPosition(new Vector3(position, position, position), axes);
		}

		public static void TranslateLocal(this Transform transform, Vector3 translation, Axes axes = Axes.XYZ)
		{
			transform.SetLocalPosition(transform.localPosition + translation, axes);
		}

		public static void TranslateLocal(this Transform transform, float translation, Axes axes = Axes.XYZ)
		{
			transform.TranslateLocal(new Vector3(translation, translation, translation), axes);
		}

		public static void TranslateLocalTowards(this Transform transform, Vector3 targetPosition, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetLocalPosition(transform.localPosition.Lerp(targetPosition, deltaTime, axes), axes);
		}

		public static void TranslateLocalTowards(this Transform transform, float targetPosition, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.TranslateLocalTowards(new Vector3(targetPosition, targetPosition, targetPosition), deltaTime, axes);
		}
		#endregion

		#region Rotation
		public static void SetEulerAngles(this Transform transform, Vector3 angles, Axes axes = Axes.XYZ)
		{
			transform.eulerAngles = transform.eulerAngles.SetValues(angles, axes);
		}

		public static void SetEulerAngles(this Transform transform, float angle, Axes axes = Axes.XYZ)
		{
			transform.SetEulerAngles(new Vector3(angle, angle, angle), axes);
		}

		public static void Rotate(this Transform transform, Vector3 rotation, Axes axes = Axes.XYZ)
		{
			transform.SetEulerAngles(transform.eulerAngles + rotation, axes);
		}

		public static void Rotate(this Transform transform, float rotation, Axes axes = Axes.XYZ)
		{
			transform.Rotate(new Vector3(rotation, rotation, rotation), axes);
		}

		public static void RotateTowards(this Transform transform, Vector3 targetAngles, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetEulerAngles(transform.eulerAngles.LerpAngles(targetAngles, deltaTime, axes), axes);
		}

		public static void RotateTowards(this Transform transform, float targetAngle, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.RotateTowards(new Vector3(targetAngle, targetAngle, targetAngle), deltaTime, axes);
		}

		public static void SetLocalEulerAngles(this Transform transform, Vector3 angles, Axes axes = Axes.XYZ)
		{
			transform.localEulerAngles = transform.localEulerAngles.SetValues(angles, axes);
		}

		public static void SetLocalEulerAngles(this Transform transform, float angle, Axes axes = Axes.XYZ)
		{
			transform.SetLocalEulerAngles(new Vector3(angle, angle, angle), axes);
		}

		public static void RotateLocal(this Transform transform, Vector3 rotation, Axes axes = Axes.XYZ)
		{
			transform.SetLocalEulerAngles(transform.localEulerAngles + rotation, axes);
		}

		public static void RotateLocal(this Transform transform, float rotation, Axes axes = Axes.XYZ)
		{
			transform.RotateLocal(new Vector3(rotation, rotation, rotation), axes);
		}

		public static void RotateLocalTowards(this Transform transform, Vector3 targetAngles, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetLocalEulerAngles(transform.localEulerAngles.LerpAngles(targetAngles, deltaTime, axes), axes);
		}

		public static void RotateLocalTowards(this Transform transform, float targetAngle, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.RotateLocalTowards(new Vector3(targetAngle, targetAngle, targetAngle), deltaTime, axes);
		}
		#endregion

		#region Scale
		public static void SetScale(this Transform transform, Vector3 scale, Axes axes = Axes.XYZ)
		{
			transform.localScale = transform.localScale.SetValues(transform.localScale.Div(transform.lossyScale, axes).Mult(scale, axes), axes);
		}

		public static void SetScale(this Transform transform, float scale, Axes axes = Axes.XYZ)
		{
			transform.SetScale(new Vector3(scale, scale, scale), axes);
		}

		public static void Scale(this Transform transform, Vector3 scale, Axes axes = Axes.XYZ)
		{
			transform.SetScale(transform.localScale + scale, axes);
		}

		public static void Scale(this Transform transform, float scale, Axes axes = Axes.XYZ)
		{
			transform.SetScale(new Vector3(scale, scale, scale), axes);
		}

		public static void ScaleTowards(this Transform transform, Vector3 targetScale, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetScale(transform.lossyScale.Lerp(targetScale, deltaTime, axes), axes);
		}

		public static void ScaleTowards(this Transform transform, float targetScale, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.ScaleTowards(new Vector3(targetScale, targetScale, targetScale), deltaTime, axes);
		}

		public static void FlipScale(this Transform transform, Axes axes = Axes.XYZ)
		{
			transform.SetScale(transform.lossyScale.SetValues(-transform.lossyScale, axes), axes);
		}

		public static void SetLocalScale(this Transform transform, Vector3 scale, Axes axes = Axes.XYZ)
		{
			transform.localScale = transform.localScale.SetValues(scale, axes);
		}

		public static void SetLocalScale(this Transform transform, float scale, Axes axes = Axes.XYZ)
		{
			transform.SetLocalScale(new Vector3(scale, scale, scale), axes);
		}

		public static void ScaleLocal(this Transform transform, Vector3 scale, Axes axes = Axes.XYZ)
		{
			transform.SetLocalScale(transform.localScale + scale, axes);
		}

		public static void ScaleLocal(this Transform transform, float scale, Axes axes = Axes.XYZ)
		{
			transform.ScaleLocal(new Vector3(scale, scale, scale), axes);
		}

		public static void ScaleLocalTowards(this Transform transform, Vector3 targetScale, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.SetLocalScale(transform.localScale.Lerp(targetScale, deltaTime, axes), axes);
		}

		public static void ScaleLocalTowards(this Transform transform, float targetScale, float deltaTime, Axes axes = Axes.XYZ)
		{
			transform.ScaleLocalTowards(new Vector3(targetScale, targetScale, targetScale), deltaTime, axes);
		}

		public static void FlipLocalScale(this Transform transform, Axes axes = Axes.XYZ)
		{
			transform.SetLocalScale(transform.localScale.SetValues(-transform.localScale, axes), axes);
		}
		#endregion

		public static bool IsRoot(this Transform transform)
		{
			return transform == transform.root;
		}

		public static Transform[] GetParents(this Transform child)
		{
			var parents = new List<Transform>();
			var parent = child.parent;

			while (parent != null)
			{
				parents.Add(parent);
				parent = parent.parent;
			}

			return parents.ToArray();
		}

		public static int GetChildCount(this Transform parent, bool recursive = false)
		{
			int childCount = 0;

			if (recursive)
			{
				for (int i = 0; i < parent.childCount; i++)
				{
					var child = parent.GetChild(i);
					childCount++;

					if (child.childCount > 0)
						childCount += child.GetChildCount(recursive);
				}
			}
			else
				childCount += parent.childCount;

			return childCount;
		}

		public static Transform[] GetChildren(this Transform parent, bool recursive = false)
		{
			var children = new List<Transform>(parent.childCount);

			for (int i = 0; i < parent.childCount; i++)
			{
				var child = parent.GetChild(i);
				children.Add(child);

				if (recursive && child.childCount > 0)
					children.AddRange(child.GetChildren(recursive));
			}

			return children.ToArray();
		}

		public static Transform FindChild(this Transform parent, string childName, bool recursive = false)
		{
			return parent.FindChild(child => child.name == childName, recursive);
		}

		public static Transform FindChild(this Transform parent, Predicate<Transform> predicate, bool recursive = false)
		{
			var children = parent.GetChildren(recursive);

			for (int i = 0; i < children.Length; i++)
			{
				var child = children[i];

				if (predicate(child))
					return child;
			}

			return null;
		}

		public static Transform[] FindChildren(this Transform parent, string childName, bool recursive = false)
		{
			return parent.FindChildren(child => child.name == childName, recursive);
		}

		public static Transform[] FindChildren(this Transform parent, Predicate<Transform> predicate, bool recursive = false)
		{
			var validChildren = new List<Transform>();
			var children = parent.GetChildren(recursive);

			for (int i = 0; i < children.Length; i++)
			{
				var child = children[i];

				if (predicate(child))
					validChildren.Add(child);
			}

			return validChildren.ToArray();
		}

		public static Transform AddChild(this Transform parent, string childName, PrimitiveType primitiveType)
		{
			var child = GameObject.CreatePrimitive(primitiveType);

			child.name = childName;
			child.transform.parent = parent;
			child.transform.Reset();

			return child.transform;
		}

		public static Transform AddChild(this Transform parent, string childName)
		{
			var child = new GameObject(childName);

			child.transform.parent = parent;
			child.transform.Reset();

			return child.transform;
		}

		public static Transform FindOrAddChild(this Transform parent, string childName, PrimitiveType primitiveType)
		{
			var child = parent.FindChild(childName);

			if (child == null)
				child = parent.AddChild(childName, primitiveType);

			return child;
		}

		public static Transform FindOrAddChild(this Transform parent, string childName)
		{
			var child = parent.FindChild(childName);

			if (child == null)
				child = parent.AddChild(childName);

			return child;
		}

		public static void SortChildren(this Transform parent, bool recursive = false)
		{
			var children = parent.GetChildren();
			var childrendNames = new string[children.Length];

			for (int i = 0; i < children.Length; i++)
				childrendNames[i] = children[i].name;

			Array.Sort(childrendNames, children);

			for (int i = 0; i < children.Length; i++)
			{
				var child = children[i];

				child.parent = null;
				child.parent = parent;

				if (recursive && child.childCount > 0)
					child.SortChildren(recursive);
			}
		}

		public static void SetChildrenActive(this Transform parent, bool value, bool recursive = false)
		{
			var children = parent.GetChildren(recursive);

			for (int i = 0; i < children.Length; i++)
				children[i].gameObject.SetActive(value);
		}

		public static void DestroyChildren(this Transform parent)
		{
			var children = parent.GetChildren();

			for (int i = 0; i < children.Length; i++)
				children[i].gameObject.Destroy();
		}

		public static int GetHierarchyDepth(this Transform transform)
		{
			int depth = 0;
			var currentTransform = transform;

			while (currentTransform.parent != null)
			{
				currentTransform = currentTransform.parent;
				depth += 1;
			}

			return depth;
		}

		public static T GetClosest<T>(this Transform transform, IList<T> targets) where T : Component
		{
			float closestDistance = float.MaxValue;
			T closestTarget = null;

			for (int i = 0; i < targets.Count; i++)
			{
				var target = targets[i];
				float distance = Vector3.Distance(transform.position, target.transform.position);

				if (distance < closestDistance)
				{
					closestTarget = target;
					closestDistance = distance;
				}
			}

			return closestTarget;
		}

		public static void ResetChildren(this Transform parent, bool recursive = false)
		{
			var children = parent.GetChildren(recursive);

			for (int i = 0; i < children.Length; i++)
				children[i].Reset();
		}

		public static void Reset(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}

		public static void Copy(this Transform target, Transform source)
		{
			target.localPosition = source.localPosition;
			target.localRotation = source.localRotation;
			target.localScale = source.localScale;
		}
	}
}
