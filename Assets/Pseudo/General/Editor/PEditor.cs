using System.Collections;
using Pseudo.Internal;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// TODO Add a UniqueNameFoldout (when selected it becomes a UniqueNameField)
namespace Pseudo.Editor.Internal
{
	public class PEditor : UnityEditor.Editor
	{
		public enum ButtonAlign
		{
			None,
			Left,
			Right,
			Center
		}

		public delegate void AddCallback(SerializedProperty arrayProperty);
		public delegate void DeleteCallback(SerializedProperty arrayProperty, int indexToRemove);
		public delegate void DropCallback<T>(T droppedObject);
		public delegate void ReorderCallback(SerializedProperty arrayProperty, int sourceIndex, int targetIndex);
		public delegate void ClearCallback(SerializedProperty property);
		public delegate void FoldoutDrawer(SerializedProperty arrayProperty);
		public delegate void ElementDrawer(SerializedProperty arrayProperty, int index, SerializedProperty property);

		public bool deleteBreak;

		public virtual void OnEnable() { }

		public virtual void OnDisable() { }

		public virtual void Begin(bool space = true)
		{
			deleteBreak = false;

			if (space)
				EditorGUILayout.Space();

			Undo.RegisterCompleteObjectUndo(target, string.Format("{0} ({1}) modified.", target.name, target.GetType()));
			EditorGUI.BeginChangeCheck();

			serializedObject.Update();
		}

		public virtual void End(bool space = true)
		{
			if (space)
				EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();

			if (EditorGUI.EndChangeCheck())
				EditorUtility.SetDirty(target);
		}

		#region Buttons
		public static bool Button(GUIContent label, GUIStyle style, ButtonAlign align, bool disableOnPlay, params GUILayoutOption[] options)
		{
			if (style == null)
			{
				style = new GUIStyle("MiniToolbarButton");
				style.clipping = TextClipping.Overflow;
				style.contentOffset = new Vector2(2, 0);
			}

			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginVertical();
			GUILayout.Space(1);
			EditorGUILayout.BeginHorizontal();

			if (align == ButtonAlign.Right || align == ButtonAlign.Center)
			{
				EditorGUILayout.Space();
			}

			bool pressed = GUILayout.Button(label, style, options);

			if (align == ButtonAlign.Left || align == ButtonAlign.Center)
			{
				EditorGUILayout.Space();
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			EditorGUI.EndDisabledGroup();

			return pressed;
		}

		public static bool Button(GUIContent label, ButtonAlign align, bool disableOnPlay, params GUILayoutOption[] options)
		{
			return Button(label, null, align, disableOnPlay, options);
		}

		public static bool Button(GUIContent label, GUIStyle style, bool disableOnPlay, params GUILayoutOption[] options)
		{
			return Button(label, style, ButtonAlign.None, disableOnPlay, options);
		}

		public static bool Button(GUIContent label, GUIStyle style, params GUILayoutOption[] options)
		{
			return Button(label, style, ButtonAlign.None, false, options);
		}

		public static bool Button(GUIContent label, bool disableOnPlay, params GUILayoutOption[] options)
		{
			return Button(label, null, ButtonAlign.None, disableOnPlay, options);
		}

		public static bool Button(GUIContent label, params GUILayoutOption[] options)
		{
			return Button(label, null, ButtonAlign.None, false, options);
		}

		public static bool LargeButton(GUIContent label, bool disableOnPlay, params GUILayoutOption[] options)
		{
			bool pressed = false;

			GUIStyle style = new GUIStyle("toolbarButton");
			style.fontStyle = FontStyle.Bold;
			style.fontSize = 10;
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(2, 0);

			pressed = Button(label, style, disableOnPlay, options);
			GUILayout.Space(2);

			return pressed;
		}

		public static bool LargeButton(GUIContent label, params GUILayoutOption[] options)
		{
			return LargeButton(label, false, options);
		}

		public static bool LargeAddButton(SerializedProperty property, GUIContent label, AddCallback addCallback, params GUILayoutOption[] options)
		{
			label = label ?? property.ToGUIContent();

			bool pressed = false;
			if (LargeButton(label, true, options))
			{
				AddToArray(property, addCallback);
				pressed = true;
			}
			return pressed;
		}

		public static bool LargeAddButton(SerializedProperty property, AddCallback addCallback, params GUILayoutOption[] options)
		{
			return LargeAddButton(property, null, addCallback, options);
		}

		public static bool LargeAddButton(SerializedProperty property, GUIContent label, params GUILayoutOption[] options)
		{
			return LargeAddButton(property, label, null, options);
		}

		public static bool LargeAddButton(SerializedProperty property, params GUILayoutOption[] options)
		{
			return LargeAddButton(property, null, null, options);
		}

		public static bool AddButton(bool disableOnPlay = true)
		{
			GUIStyle style = new GUIStyle("toolbarbutton");
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(0, -1);
			style.fontSize = 10;

			bool pressed = Button("+".ToGUIContent(), style, ButtonAlign.Right, disableOnPlay, GUILayout.Width(16));

			return pressed;
		}

		public static bool AddButton(SerializedProperty property, bool disabeOnPlay = true, AddCallback addCallback = null)
		{
			bool pressed = false;
			if (AddButton(disabeOnPlay))
			{
				AddToArray(property, addCallback);
				pressed = true;
			}
			return pressed;
		}

		public static bool SmallAddButton()
		{
			GUIStyle style = new GUIStyle("MiniToolbarButtonLeft");
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(-1, -1);
			style.fontSize = 10;

			bool pressed = Button("+".ToGUIContent(), style, ButtonAlign.Right, true, GUILayout.Width(16));

			return pressed;
		}

		public static bool SmallAddButton(SerializedProperty property, AddCallback addCallback = null)
		{
			bool pressed = false;

			if (SmallAddButton())
			{
				AddToArray(property, addCallback);
				pressed = true;
			}

			return pressed;
		}

		public static bool DeleteButton(ButtonAlign align, bool disabeOnPlay = true)
		{
			var style = new GUIStyle("MiniToolbarButtonLeft")
			{
				clipping = TextClipping.Overflow
			};

			bool pressed = Button("−".ToGUIContent(), style, align, disabeOnPlay, GUILayout.Width(16));

			return pressed;
		}

		public static bool DeleteButton(bool disabeOnPlay = true)
		{
			return DeleteButton(ButtonAlign.Right, disabeOnPlay);
		}

		public bool DeleteButton(SerializedProperty property, int indexToRemove, ButtonAlign align, bool disabeOnPlay = true, DeleteCallback deleteCallback = null)
		{
			bool pressed = false;

			if (DeleteButton(align, disabeOnPlay))
			{
				DeleteFromArray(property, indexToRemove, deleteCallback);
				pressed = true;
			}

			return pressed;
		}

		public bool DeleteButton(SerializedProperty property, int indexToRemove, bool disabeOnPlay = true, DeleteCallback deleteCallback = null)
		{
			return DeleteButton(property, indexToRemove, ButtonAlign.Right, disabeOnPlay, deleteCallback);
		}

		public static string FolderPathButton(string path, string relativeTo, GUIContent label, params GUILayoutOption[] options)
		{
			Rect rect = EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(" ", options);
			EditorGUILayout.EndHorizontal();

			rect = EditorGUI.PrefixLabel(rect, label);

			GUIStyle buttonStyle = new GUIStyle("TL SelectionButton");
			buttonStyle.font = EditorStyles.miniFont;
			buttonStyle.fontStyle = FontStyle.Italic;
			buttonStyle.clipping = TextClipping.Clip;
			buttonStyle.normal.textColor = EditorStyles.label.normal.textColor;

			if (GUI.Button(rect, path.ToGUIContent(path), buttonStyle))
			{
				path = EditorUtility.OpenFolderPanel("Select Folder", relativeTo + path, "");

				if (!string.IsNullOrEmpty(relativeTo))
				{
					if (path.StartsWith(relativeTo))
					{
						path = path.Substring(relativeTo.Length);
					}
					else if (!string.IsNullOrEmpty(path))
					{
						Debug.LogWarning(string.Format("The relative directory ({0}) does not contain the selected folder ({1}).", relativeTo, path));
						path = "";
					}
				}
			}

			return path;
		}

		public static string FolderPathButton(string path, string relativeTo, params GUILayoutOption[] options)
		{
			return FolderPathButton(path, relativeTo, GUIContent.none, options);
		}

		public static string FolderPathButton(string path, GUIContent label, params GUILayoutOption[] options)
		{
			return FolderPathButton(path, "", label, options);
		}

		public static string FolderPathButton(string path, params GUILayoutOption[] options)
		{
			return FolderPathButton(path, "", GUIContent.none, options);
		}

		public static void FolderPathButton(SerializedProperty stringProperty, string relativeTo, GUIContent label, params GUILayoutOption[] options)
		{
			label = label ?? stringProperty.ToGUIContent();

			stringProperty.SetValue(FolderPathButton(stringProperty.GetValue<string>(), relativeTo, label, options));
		}

		public static void FolderPathButton(SerializedProperty stringProperty, string relativeTo, params GUILayoutOption[] options)
		{
			FolderPathButton(stringProperty, relativeTo, null, options);
		}

		public static void FolderPathButton(SerializedProperty stringProperty, GUIContent label, params GUILayoutOption[] options)
		{
			FolderPathButton(stringProperty, "", label, options);
		}

		public static void FolderPathButton(SerializedProperty stringProperty, params GUILayoutOption[] options)
		{
			FolderPathButton(stringProperty, "", null, options);
		}

		public static string FilePathButton(string path, string relativeTo, string extension, GUIContent label, params GUILayoutOption[] options)
		{
			Rect rect = EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(" ", options);
			EditorGUILayout.EndHorizontal();

			rect = EditorGUI.PrefixLabel(rect, label);

			GUIStyle buttonStyle = new GUIStyle("TL SelectionButton");
			buttonStyle.font = EditorStyles.miniFont;
			buttonStyle.fontStyle = FontStyle.Italic;
			buttonStyle.clipping = TextClipping.Clip;
			buttonStyle.normal.textColor = EditorStyles.label.normal.textColor;

			if (GUI.Button(rect, path.ToGUIContent(path), buttonStyle))
			{
				path = EditorUtility.OpenFilePanel("Select File", relativeTo + path, extension);

				if (!string.IsNullOrEmpty(relativeTo))
				{
					if (path.StartsWith(relativeTo))
					{
						path = path.Substring(relativeTo.Length);
					}
					else if (!string.IsNullOrEmpty(path))
					{
						Debug.LogWarning(string.Format("The relative directory ({0}) does not contain the selected file ({1}).", relativeTo, path));
						path = "";
					}
				}
			}


			return path;
		}

		public static string FilePathButton(string path, string relativeTo, string extension, params GUILayoutOption[] options)
		{
			return FilePathButton(path, relativeTo, extension, GUIContent.none, options);
		}

		public static string FilePathButton(string path, string relativeTo, params GUILayoutOption[] options)
		{
			return FilePathButton(path, relativeTo, "", GUIContent.none, options);
		}

		public static string FilePathButton(string path, string relativeTo, GUIContent label, params GUILayoutOption[] options)
		{
			return FilePathButton(path, "", relativeTo, label, options);
		}

		public static string FilePathButton(string path, GUIContent label, params GUILayoutOption[] options)
		{
			return FilePathButton(path, "", "", label, options);
		}

		public static string FilePathButton(string path, params GUILayoutOption[] options)
		{
			return FilePathButton(path, "", "", GUIContent.none, options);
		}

		public static void FilePathButton(SerializedProperty stringProperty, string relativeTo, string extension, GUIContent label, params GUILayoutOption[] options)
		{
			label = label ?? stringProperty.ToGUIContent();

			stringProperty.SetValue(FilePathButton(stringProperty.GetValue<string>(), relativeTo, extension, label, options));
		}

		public static void FilePathButton(SerializedProperty stringProperty, string relativeTo, string extension, params GUILayoutOption[] options)
		{
			FilePathButton(stringProperty, relativeTo, extension, null, options);
		}

		public static void FilePathButton(SerializedProperty stringProperty, string relativeTo, GUIContent label, params GUILayoutOption[] options)
		{
			FilePathButton(stringProperty, relativeTo, "", label, options);
		}

		public static void FilePathButton(SerializedProperty stringProperty, string relativeTo, params GUILayoutOption[] options)
		{
			FilePathButton(stringProperty, relativeTo, "", null, options);
		}

		public static void FilePathButton(SerializedProperty stringProperty, GUIContent label, params GUILayoutOption[] options)
		{
			FilePathButton(stringProperty, "", "", label, options);
		}

		public static void FilePathButton(SerializedProperty stringProperty, params GUILayoutOption[] options)
		{
			FilePathButton(stringProperty, "", "", null, options);
		}
		#endregion

		#region Foldouts
		public static void Foldout(SerializedProperty property, GUIContent label, GUIStyle style)
		{
			label = label ?? property.ToGUIContent();
			style = style ?? EditorStyles.foldout;

			EditorGUILayout.BeginHorizontal();
			property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, style);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
		}

		public static void Foldout(SerializedProperty property, GUIContent label)
		{
			Foldout(property, label, null);
		}

		public static void Foldout(SerializedProperty property)
		{
			Foldout(property, null, null);
		}

		public static void Foldout(IShowable showable, GUIContent label, GUIStyle style)
		{
			style = style ?? EditorStyles.foldout;

			EditorGUILayout.BeginHorizontal();
			showable.Showing = EditorGUILayout.Foldout(showable.Showing, label, style);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
		}

		public static void Foldout(IShowable showable, GUIContent label)
		{
			Foldout(showable, label, null);
		}

		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, GUIStyle style, DropCallback<T> dropCallback) where T : Object
		{
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			Foldout(property, label, style);
			EditorGUILayout.EndHorizontal();

			DropArea<T>(dropArea, disableOnPlay, dropCallback);
		}

		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(property, disableOnPlay, label, null, dropCallback);
		}

		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(property, disableOnPlay, null, null, dropCallback);
		}

		public void DropFoldout<T>(SerializedProperty property, GUIContent label, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(property, false, label, null, dropCallback);
		}

		public void DropFoldout<T>(SerializedProperty property, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(property, false, null, null, dropCallback);
		}

		public void DropFoldout<T>(IShowable showable, bool disableOnPlay, GUIContent label, GUIStyle style, DropCallback<T> dropCallback) where T : Object
		{
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			Foldout(showable, label, style);
			EditorGUILayout.EndHorizontal();

			DropArea<T>(dropArea, disableOnPlay, dropCallback);
		}

		public void DropFoldout<T>(IShowable showable, bool disableOnPlay, GUIContent label, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(showable, disableOnPlay, label, null, dropCallback);
		}

		public void DropFoldout<T>(IShowable showable, GUIContent label, DropCallback<T> dropCallback) where T : Object
		{
			DropFoldout<T>(showable, false, label, null, dropCallback);
		}

		public void ArrayFoldout(SerializedProperty arrayProperty, GUIContent label = null, GUIStyle style = null, FoldoutDrawer foldoutDrawer = null, ElementDrawer elementDrawer = null, ElementDrawer onPreElementDraw = null, ElementDrawer onPostElementDraw = null, bool disableOnPlay = true, AddCallback addCallback = null, DeleteCallback deleteCallback = null, ReorderCallback reorderCallback = null)
		{
			if (foldoutDrawer == null)
				AddFoldOut(arrayProperty, -1, label, style, disableOnPlay, addCallback);
			else
				foldoutDrawer(arrayProperty);

			if (arrayProperty.isExpanded)
			{
				EditorGUI.indentLevel++;

				for (int i = 0; i < arrayProperty.arraySize; i++)
				{
					SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(i);

					if (onPreElementDraw != null)
						onPreElementDraw(arrayProperty, i, elementProperty);

					Rect rect = EditorGUI.IndentedRect(EditorGUILayout.BeginVertical());
					rect.width += EditorGUI.indentLevel * 2f;

					var buttonStyle = new GUIStyle("MiniToolbarButtonLeft")
					{
						clipping = TextClipping.Overflow
					};

					EditorGUI.BeginDisabledGroup(disableOnPlay && Application.isPlaying);

					if (GUI.Button(new Rect(rect.x + rect.width - 18f, rect.y + 1f, 16f, 16f), "−".ToGUIContent(), buttonStyle))
					{
						DeleteFromArray(arrayProperty, i, deleteCallback);
						break;
					}

					EditorGUI.EndDisabledGroup();

					if (Reorderable(arrayProperty, i, disableOnPlay, new Rect(rect.x + 8f, rect.y, rect.width - 24f, 15f), new Rect(rect.x, rect.y, rect.width - 1f, 15f), reorderCallback))
						break;

					if (elementDrawer == null)
						EditorGUILayout.PropertyField(elementProperty, true);
					else
						elementDrawer(arrayProperty, i, elementProperty);

					EditorGUILayout.EndVertical();

					if (onPostElementDraw != null)
						onPostElementDraw(arrayProperty, i, elementProperty);
				}

				EditorGUI.indentLevel--;
			}
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, GUIStyle style, AddCallback addCallback = null)
		{
			label.text += string.Format(" ({0})", GetArraySize(arrayProperty, overrideArraySize));

			EditorGUILayout.BeginHorizontal();

			if (showable.Showing && GetArraySize(arrayProperty, overrideArraySize) == 0)
			{
				showable.Showing = false;
			}

			Foldout(showable, label, style);

			bool pressed = false;
			if (showable.Showing && GetArraySize(arrayProperty, overrideArraySize) == 0 && !Application.isPlaying)
			{
				AddToArray(arrayProperty, addCallback);
				pressed = true;
			}

			if (AddButton(arrayProperty, true, addCallback))
			{
				pressed = true;
			}

			EditorGUILayout.EndHorizontal();
			return pressed;
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, showable, overrideArraySize, label, null, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, showable, overrideArraySize, null, null, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, GUIContent label, GUIStyle style, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, showable, -1, label, style, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, GUIContent label, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, showable, -1, label, null, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, showable, -1, null, null, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, GUIStyle style, bool disableOnPlay = true, AddCallback addCallback = null)
		{
			label = label ?? arrayProperty.ToGUIContent();
			label.text += string.Format(" ({0})", GetArraySize(arrayProperty, overrideArraySize));

			EditorGUILayout.BeginHorizontal();

			if (arrayProperty.isExpanded && GetArraySize(arrayProperty, overrideArraySize) == 0)
				arrayProperty.isExpanded = false;

			Foldout(arrayProperty, label, style);

			bool pressed = false;
			if (arrayProperty.isExpanded && GetArraySize(arrayProperty, overrideArraySize) == 0 && !(disableOnPlay && Application.isPlaying))
			{
				AddToArray(arrayProperty, addCallback);
				pressed = true;
			}

			if (AddButton(arrayProperty, disableOnPlay, addCallback))
				pressed = true;

			EditorGUILayout.EndHorizontal();

			return pressed;
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, overrideArraySize, label, null, true, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, overrideArraySize, null, null, true, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, GUIContent label, GUIStyle style, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, -1, label, style, true, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, GUIContent label, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, -1, label, null, true, addCallback);
		}

		public static bool AddFoldOut(SerializedProperty arrayProperty, AddCallback addCallback = null)
		{
			return AddFoldOut(arrayProperty, -1, null, null, true, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			bool pressed = AddFoldOut(arrayProperty, showable, overrideArraySize, label, style, addCallback);
			EditorGUILayout.EndHorizontal();

			DropArea<T>(dropArea, true, dropCallback);

			return pressed;
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut<T>(arrayProperty, showable, overrideArraySize, label, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut<T>(arrayProperty, showable, overrideArraySize, null, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut<T>(arrayProperty, showable, -1, label, style, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut<T>(arrayProperty, showable, -1, label, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut<T>(arrayProperty, showable, -1, null, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			Rect dropArea = EditorGUILayout.BeginVertical();
			bool pressed = AddFoldOut(arrayProperty, overrideArraySize, label, style, true, addCallback);
			EditorGUILayout.EndVertical();

			DropArea(dropArea, true, dropCallback);

			return pressed;
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut(arrayProperty, overrideArraySize, label, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut(arrayProperty, overrideArraySize, null, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut(arrayProperty, -1, label, style, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut(arrayProperty, -1, label, null, dropCallback, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object
		{
			return AddFoldOut(arrayProperty, -1, null, null, dropCallback, addCallback);
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, ReorderCallback reorderCallback, bool disabeOnPlay, DeleteCallback deleteCallback = null)
		{
			EditorGUILayout.BeginHorizontal();

			SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(index);
			Foldout(elementProperty, label, style);
			bool pressed = DeleteButton(arrayProperty, index, disabeOnPlay, deleteCallback);

			EditorGUILayout.EndHorizontal();

			if (!pressed)
				Reorderable(arrayProperty, index, true, reorderCallback);

			return pressed;
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null)
		{
			return DeleteFoldOut(arrayProperty, index, label, null, reorderCallback, true, deleteCallback);
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null)
		{
			return DeleteFoldOut(arrayProperty, index, null, null, reorderCallback, true, deleteCallback);
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DeleteCallback deleteCallback = null)
		{
			return DeleteFoldOut(arrayProperty, index, label, style, null, true, deleteCallback);
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, DeleteCallback deleteCallback = null)
		{
			return DeleteFoldOut(arrayProperty, index, label, null, null, true, deleteCallback);
		}

		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, DeleteCallback deleteCallback = null)
		{
			return DeleteFoldOut(arrayProperty, index, null, null, null, true, deleteCallback);
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			bool pressed = DeleteFoldOut(arrayProperty, index, label, style, reorderCallback, true, deleteCallback);
			EditorGUILayout.EndHorizontal();

			DropArea<T>(dropArea, true, dropCallback);

			return pressed;
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			return DeleteFoldOut<T>(arrayProperty, index, label, null, dropCallback, reorderCallback, deleteCallback);
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			return DeleteFoldOut<T>(arrayProperty, index, null, null, dropCallback, reorderCallback, deleteCallback);
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			return DeleteFoldOut<T>(arrayProperty, index, label, style, dropCallback, null, deleteCallback);
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			return DeleteFoldOut<T>(arrayProperty, index, label, null, dropCallback, null, deleteCallback);
		}

		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object
		{
			return DeleteFoldOut<T>(arrayProperty, index, null, null, dropCallback, null, deleteCallback);
		}
		#endregion

		#region Miscellaneous
		public static object ObjectField(object value, GUIContent label)
		{
			if (value == null)
				return null;

			var dummy = DummyUtility.GetDummy(value);
			var serializedDummy = DummyUtility.SerializeDummy(dummy);

			if (serializedDummy == null)
				return null;

			EditorGUI.BeginChangeCheck();

			EditorGUILayout.PropertyField(serializedDummy, label, true);

			if (EditorGUI.EndChangeCheck())
			{
				serializedDummy.serializedObject.ApplyModifiedProperties();
				value = dummy.Value;
			}

			return value;
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			label = label ?? property.ToGUIContent();

			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginHorizontal();

			EditorGUI.BeginChangeCheck();
			property.objectReferenceValue = EditorGUILayout.ObjectField(label, property.objectReferenceValue, typeof(T), allowSceneObjects, options);
			if (EditorGUI.EndChangeCheck())
			{
				property.serializedObject.ApplyModifiedProperties();
			}

			ContextMenu(new[] { "Clear".ToGUIContent() }, new GenericMenu.MenuFunction2[] { OnPropertyCleared }, new object[] { new ContextMenuClearData(property, clearCallback) });

			EditorGUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, allowSceneObjects, null, clearCallback, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, GUIContent label, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, allowSceneObjects, label, null, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, true, label, clearCallback, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, true, null, clearCallback, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, true, label, null, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, disableOnPlay, true, null, null, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, false, true, label, clearCallback, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, false, true, null, clearCallback, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, GUIContent label, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, false, true, label, null, options);
		}

		public void PropertyObjectField<T>(SerializedProperty property, params GUILayoutOption[] options) where T : Object
		{
			PropertyObjectField<T>(property, false, true, null, null, options);
		}

		public bool UniqueNameField(INamable namable, INamable[] namables, GUIContent label, GUIStyle style, params GUILayoutOption[] options)
		{
			style = style ?? new GUIStyle("textField");
			bool changed = false;

			EditorGUI.BeginDisabledGroup(Application.isPlaying);
			EditorGUI.BeginChangeCheck();

			string currentName = EditorGUILayout.TextField(label, namable.Name, style, options);

			if (EditorGUI.EndChangeCheck())
			{
				INamableExtensions.SetUniqueName(namable, namables, currentName, namable.Name, "default");
				changed = true;
			}

			EditorGUI.EndDisabledGroup();

			return changed;
		}

		public bool UniqueNameField(INamable namable, INamable[] namables, GUIContent label, params GUILayoutOption[] options)
		{
			return UniqueNameField(namable, namables, label, null, options);
		}

		public bool UniqueNameField(INamable namable, INamable[] namables, params GUILayoutOption[] options)
		{
			return UniqueNameField(namable, namables, GUIContent.none, null, options);
		}

		public static void MinMaxSlider(SerializedProperty minProperty, SerializedProperty maxProperty, float min, float max, bool disableOnPlay)
		{
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			float minValue = 0;
			if (minProperty.propertyType == SerializedPropertyType.Integer)
				minValue = (int)minProperty.GetValue();
			else if (minProperty.propertyType == SerializedPropertyType.Float)
				minValue = (float)minProperty.GetValue();

			float maxValue = 0;
			if (maxProperty.propertyType == SerializedPropertyType.Integer)
				maxValue = (int)maxProperty.GetValue();
			else if (maxProperty.propertyType == SerializedPropertyType.Float)
				maxValue = (float)maxProperty.GetValue();

			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(indent * 15));

			minValue = EditorGUILayout.FloatField(minValue, GUILayout.MaxWidth((Screen.width - EditorGUI.indentLevel * 15) * 0.125F));
			EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, min, max);
			maxValue = EditorGUILayout.FloatField(maxValue, GUILayout.MaxWidth((Screen.width - EditorGUI.indentLevel * 15) * 0.125F));

			EditorGUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();

			minValue = Mathf.Clamp(minValue, min, maxValue);
			maxValue = Mathf.Clamp(maxValue, minValue, max);

			if (minProperty.propertyType == SerializedPropertyType.Integer)
			{
				minProperty.SetValue((int)minValue);
			}
			else if (minProperty.propertyType == SerializedPropertyType.Float)
			{
				minProperty.SetValue(minValue);
			}

			if (maxProperty.propertyType == SerializedPropertyType.Integer)
			{
				maxProperty.SetValue((int)maxValue);
			}
			else if (maxProperty.propertyType == SerializedPropertyType.Float)
			{
				maxProperty.SetValue(maxValue);
			}

			EditorGUI.indentLevel = indent;
		}

		public static void MinMaxSlider(SerializedProperty minProperty, SerializedProperty maxProperty, float min, float max)
		{
			MinMaxSlider(minProperty, maxProperty, min, max, false);
		}

		public static string Popup(string currentOption, string[] displayedOptions, GUIContent label, GUIStyle style, params GUILayoutOption[] options)
		{
			style = style ?? new GUIStyle("MiniPopup");

			int index = System.Array.IndexOf(displayedOptions, currentOption);
			int currentIndex = EditorGUILayout.Popup(label, index, displayedOptions.ToGUIContents(), style, options);

			return displayedOptions == null || displayedOptions.Length == 0 ? "" : displayedOptions[Mathf.Clamp(currentIndex, 0, Mathf.Max(displayedOptions.Length - 1, 0))];
		}

		public static string Popup(string currentOption, string[] displayedOptions, GUIContent label, params GUILayoutOption[] options)
		{
			return Popup(currentOption, displayedOptions, label, null, options);
		}

		public static string Popup(string currentOption, string[] displayedOptions, params GUILayoutOption[] options)
		{
			return Popup(currentOption, displayedOptions, GUIContent.none, options);
		}

		public static void Popup(SerializedProperty stringProperty, string[] displayedOptions, GUIContent label, GUIStyle style, params GUILayoutOption[] options)
		{
			label = label ?? stringProperty.ToGUIContent();

			stringProperty.SetValue(Popup(stringProperty.stringValue, displayedOptions, label, style, options));
		}

		public static void Popup(SerializedProperty stringProperty, string[] displayedOptions, GUIContent label, params GUILayoutOption[] options)
		{
			Popup(stringProperty, displayedOptions, label, null, options);
		}

		public static void Popup(SerializedProperty stringProperty, string[] displayedOptions, params GUILayoutOption[] options)
		{
			Popup(stringProperty, displayedOptions, null, null, options);
		}

		public static bool DragArea(Rect dragArea, Object dragObject, bool disableOnPlay)
		{
			if (Application.isPlaying && disableOnPlay)
				return false;

			GUIStyle selectedStyle = new GUIStyle("TL SelectionButton PreDropGlow");
			bool isDragged = DragAndDrop.objectReferences.Contains(dragObject);

			if (Event.current.type == EventType.MouseDown && Event.current.type != EventType.ContextClick && dragArea.Contains(Event.current.mousePosition))
			{
				DragAndDrop.PrepareStartDrag();
				DragAndDrop.StartDrag(string.Format("Dragging object {0}.", dragObject));
				DragAndDrop.objectReferences = new[] { dragObject };
				Event.current.Use();
			}

			if (isDragged)
			{
				GUI.Label(dragArea, GUIContent.none, selectedStyle);
			}

			return isDragged;
		}

		public static void DragArea(Rect dragArea, Object dragObject)
		{
			DragArea(dragArea, dragObject, true);
		}

		public static void DragArea(Object dragObject, bool disableOnPlay)
		{
			DragArea(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), dragObject, disableOnPlay);
		}

		public static void DragArea(Object dragObject)
		{
			DragArea(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), dragObject, true);
		}

		public static void DropArea<T>(Rect dropArea, bool disableOnPlay, DropCallback<T> dropCallback) where T : Object
		{
			if (Application.isPlaying && disableOnPlay)
				return;

			GUIStyle targetStyle = new GUIStyle("ColorPickerBox");

			if (dropArea.Contains(Event.current.mousePosition))
			{
				if (DragAndDrop.objectReferences != null && DragAndDrop.objectReferences.Length > 0)
				{
					GameObject gameObject = DragAndDrop.objectReferences[0] as GameObject;

					T dropTarget = typeof(T) == typeof(GameObject) ? gameObject as T : DragAndDrop.objectReferences[0] as T;

					if (dropTarget == null && typeof(Component).IsAssignableFrom(typeof(T)))
						dropTarget = gameObject == null ? default(T) : gameObject.GetComponent(typeof(T)) as T;

					if (dropTarget != null)
					{
						DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
						GUI.Label(dropArea, GUIContent.none, targetStyle);
					}
				}

				if (Event.current.type == EventType.DragPerform)
				{
					DragAndDrop.AcceptDrag();

					foreach (Object droppedElement in DragAndDrop.objectReferences)
					{
						GameObject gameObject = droppedElement as GameObject;
						T dropTarget = typeof(T) == typeof(GameObject) ? gameObject as T : droppedElement as T;

						if (dropTarget == null)
							dropTarget = gameObject == null ? default(T) : gameObject.GetComponent(typeof(T)) as T;

						if (dropTarget != null)
							dropCallback(dropTarget);
					}

					DragAndDrop.PrepareStartDrag();
					Event.current.Use();
				}
			}
		}

		public static void DropArea<T>(Rect dropArea, DropCallback<T> dropCallback) where T : Object
		{
			DropArea<T>(dropArea, false, dropCallback);
		}

		public static void DropArea<T>(bool disableOnPlay, DropCallback<T> dropCallback) where T : Object
		{
			DropArea<T>(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), disableOnPlay, dropCallback);
		}

		public static void DropArea<T>(DropCallback<T> dropCallback) where T : Object
		{
			DropArea<T>(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), false, dropCallback);
		}

		public static void ContextMenu(Rect contextArea, GUIContent[] options, GenericMenu.MenuFunction[] callbacks)
		{
			if (Event.current.type == EventType.ContextClick)
			{
				if (contextArea.Contains(Event.current.mousePosition))
				{
					GenericMenu menu = new GenericMenu();

					for (int i = 0; i < options.Length; i++)
						menu.AddItem(options[i], false, callbacks[i]);

					menu.ShowAsContext();
					Event.current.Use();
				}
			}
		}

		public static void ContextMenu(GUIContent[] options, GenericMenu.MenuFunction[] callbacks)
		{
			ContextMenu(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), options, callbacks);
		}

		public static void ContextMenu(Rect contextArea, GUIContent[] options, GenericMenu.MenuFunction2[] callbacks, object[] data)
		{
			if (Event.current.type == EventType.ContextClick)
			{
				if (contextArea.Contains(Event.current.mousePosition))
				{
					GenericMenu menu = new GenericMenu();

					for (int i = 0; i < options.Length; i++)
						menu.AddItem(options[i], false, callbacks[i], data[i]);

					menu.ShowAsContext();
					Event.current.Use();
				}
			}
		}

		public static void ContextMenu(GUIContent[] options, GenericMenu.MenuFunction2[] callbacks, object[] data)
		{
			ContextMenu(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), options, callbacks, data);
		}

		public static bool Reorderable(SerializedProperty arrayProperty, int index, bool disableOnPlay, Rect dragArea, Rect appearanceArea, ReorderCallback reorderCallback = null)
		{
			if (Application.isPlaying && disableOnPlay || arrayProperty.arraySize <= 1)
				return false;

			int arrayId = arrayProperty.GetPropertyHash();
			var selectedArray = DragAndDrop.GetGenericData(arrayId.ToString()) as SerializedProperty;
			int selectedIndex = DragAndDrop.GetGenericData("Selected Index") == null ? -1 : (int)DragAndDrop.GetGenericData("Selected Index");
			int targetIndex = DragAndDrop.GetGenericData("Target Index") == null ? -1 : (int)DragAndDrop.GetGenericData("Target Index");
			GUIStyle selectedStyle = new GUIStyle("TL SelectionButton PreDropGlow");
			GUIStyle targetStyle = new GUIStyle("ColorPickerBox");

			switch (Event.current.type)
			{
				case EventType.MouseDown:
					//if (dragArea.Contains(Event.current.mousePosition))
					//{
					//	DragAndDrop.PrepareStartDrag();
					//	DragAndDrop.SetGenericData(arrayId.ToString(), arrayProperty);
					//	DragAndDrop.SetGenericData("Selected Index", index);
					//	DragAndDrop.objectReferences = new Object[] { new Object() };
					//	Event.current.Use();
					//}
					break;
				case EventType.MouseDrag:
					if (selectedArray != null && selectedIndex == index)
					{
						DragAndDrop.StartDrag(string.Format("Dragging array element {0} at index {1}.", arrayProperty.name, index));
						Event.current.Use();
					}
					else if (dragArea.Contains(Event.current.mousePosition))
					{
						DragAndDrop.PrepareStartDrag();
						DragAndDrop.SetGenericData(arrayId.ToString(), arrayProperty);
						DragAndDrop.SetGenericData("Selected Index", index);
						DragAndDrop.objectReferences = new Object[] { new Object() };
						Event.current.Use();
					}
					break;
				case EventType.MouseUp:
					if (selectedArray != null && selectedIndex == index)
					{
						DragAndDrop.PrepareStartDrag();
						Event.current.Use();
					}
					break;
				case EventType.DragUpdated:
					if (selectedArray != null)
					{
						if (selectedIndex == index)
							GUI.Label(appearanceArea, GUIContent.none, selectedStyle);
						else if (selectedIndex != -1 && dragArea.Contains(Event.current.mousePosition))
						{
							DragAndDrop.visualMode = DragAndDropVisualMode.Move;
							DragAndDrop.SetGenericData("Target Index", index);
							GUI.Label(appearanceArea, GUIContent.none, targetStyle);
							Event.current.Use();
						}
					}
					break;
				case EventType.DragPerform:
					if (selectedArray != null && selectedIndex != -1 && dragArea.Contains(Event.current.mousePosition))
					{
						DragAndDrop.AcceptDrag();
						ReorderArray(arrayProperty, selectedIndex, targetIndex, reorderCallback);
						Event.current.Use();

						return true;
					}
					break;
				case EventType.DragExited:
					if (selectedArray != null && selectedIndex == index)
					{
						DragAndDrop.PrepareStartDrag();
						Event.current.Use();
					}
					break;
				case EventType.Repaint:
					if (selectedArray != null)
					{
						if (selectedIndex == index)
							GUI.Label(appearanceArea, GUIContent.none, selectedStyle);
						else if (selectedIndex != -1 && dragArea.Contains(Event.current.mousePosition))
						{
							DragAndDrop.visualMode = DragAndDropVisualMode.Move;
							DragAndDrop.SetGenericData("Target Index", index);
							GUI.Label(appearanceArea, GUIContent.none, targetStyle);
						}
					}
					break;
			}

			return false;
		}

		public static bool Reorderable(SerializedProperty arrayProperty, int index, bool disableOnPlay, Rect dragArea, ReorderCallback reorderCallback = null)
		{
			return Reorderable(arrayProperty, index, disableOnPlay, dragArea, dragArea, reorderCallback);
		}

		public static bool Reorderable(SerializedProperty arrayProperty, int index, Rect dragArea, ReorderCallback reorderCallback = null)
		{
			return Reorderable(arrayProperty, index, false, dragArea, reorderCallback);
		}

		public static bool Reorderable(SerializedProperty arrayProperty, int index, bool disableOnPlay, ReorderCallback reorderCallback = null)
		{
			return Reorderable(arrayProperty, index, disableOnPlay, EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), reorderCallback);
		}

		public static bool Reorderable(SerializedProperty arrayProperty, int index, ReorderCallback reorderCallback = null)
		{
			return Reorderable(arrayProperty, index, EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), reorderCallback);
		}

		public static void Separator(bool reserveVerticalSpace = true)
		{
			if (reserveVerticalSpace)
			{
				GUILayout.Space(4);
				EditorGUILayout.LabelField(GUIContent.none, new GUIStyle("RL DragHandle"), GUILayout.Height(4));
				GUILayout.Space(4);
			}
			else
			{
				Rect position = EditorGUILayout.BeginVertical();
				position.y += 7;
				EditorGUI.LabelField(position, GUIContent.none, new GUIStyle("RL DragHandle"));
				EditorGUILayout.EndVertical();
			}
		}

		public static void Space(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				GUILayout.Label("");
			}
		}

		public static void Errors(Rect rect, List<GUIContent> errors, GenericMenu.MenuFunction errorCallback = null)
		{
			errorCallback = errorCallback == null ? () => { } : errorCallback;

			if (errors.Count > 0)
			{
				rect.width = 21f;
				rect.height = 21f;
				var errorIcon = new GUIStyle("Wizard Error").normal.background;
				if (GUI.Button(rect, new GUIContent(errorIcon), new GUIStyle()))
				{
					var menu = new GenericMenu();

					for (int i = 0; i < errors.Count; i++)
						menu.AddItem(errors[i], false, errorCallback);

					menu.DropDown(rect);
				}
			}
		}

		public static void Errors(Rect rect, List<GUIContent> errors, List<object> data, GenericMenu.MenuFunction2 errorCallback = null)
		{
			data.Resize(errors.Count);
			errorCallback = errorCallback == null ? d => { } : errorCallback;

			if (errors.Count > 0)
			{
				rect.width = 21f;
				rect.height = 21f;
				var errorIcon = new GUIStyle("Wizard Error").normal.background;
				if (GUI.Button(rect, new GUIContent(errorIcon), new GUIStyle()))
				{
					var menu = new GenericMenu();

					for (int i = 0; i < errors.Count; i++)
						menu.AddItem(errors[i], false, errorCallback, data[i]);

					menu.DropDown(rect);
				}
			}
		}
		#endregion

		#region Utility
		public static void BeginBox(GUIStyle style)
		{
			Rect rect = EditorGUILayout.BeginVertical();
			rect.x += EditorGUI.indentLevel * 15;
			rect.width -= EditorGUI.indentLevel * 15 - 2;
			rect.height += 1;

			GUI.Box(rect, "", style);
		}

		public static void BeginBox()
		{
			BeginBox(new GUIStyle("box"));
		}

		public static void EndBox()
		{
			EditorGUILayout.EndVertical();
		}

		public static void AddToArray(SerializedProperty arrayProperty, AddCallback addCallback = null)
		{
			if (addCallback == null)
				arrayProperty.Add(null);
			else
				addCallback(arrayProperty);
		}

		public void DeleteFromArray(SerializedProperty arrayProperty, int indexToRemove, DeleteCallback deleteCallback = null)
		{
			if (deleteCallback == null)
				arrayProperty.RemoveAt(indexToRemove);
			else
				deleteCallback(arrayProperty, indexToRemove);

			deleteBreak = true;
		}

		public void Clear(SerializedProperty property, ClearCallback clearCallback = null)
		{
			if (clearCallback == null)
			{
				if (property.isArray)
					ClearArray(property);
				else
				{
					property.SetValue(null);
					property.serializedObject.ApplyModifiedProperties();
					EditorUtility.SetDirty(property.serializedObject.targetObject);
				}
			}
			else
				clearCallback(property);

			deleteBreak = true;
		}

		public void ClearArray(SerializedProperty arrayProperty, ClearCallback clearCallback = null)
		{
			if (clearCallback == null)
			{
				arrayProperty.ClearArray();
				arrayProperty.serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else
				clearCallback(arrayProperty);

			deleteBreak = true;
		}

		public static void ReorderArray(SerializedProperty arrayProperty, int sourceIndex, int targetIndex, ReorderCallback reorderCallback = null)
		{
			if (reorderCallback == null)
			{
				arrayProperty.MoveArrayElement(sourceIndex, targetIndex);
				arrayProperty.serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else
				reorderCallback(arrayProperty, sourceIndex, targetIndex);
		}

		public static int GetArraySize(SerializedProperty property, int overrideArraySize)
		{
			int arraySize = property.arraySize;

			if (overrideArraySize >= 0)
				arraySize = overrideArraySize;

			return arraySize;
		}
		#endregion

		void OnPropertyCleared(object data)
		{
			ContextMenuClearData contextData = data as ContextMenuClearData;
			Clear(contextData.property, contextData.clearCallback);
		}

		public class ContextMenuClearData
		{
			public readonly SerializedProperty property;
			public readonly ClearCallback clearCallback;

			public ContextMenuClearData(SerializedProperty property, ClearCallback clearCallback)
			{
				this.property = property;
				this.clearCallback = clearCallback;
			}

			public ContextMenuClearData(SerializedProperty property)
			{
				this.property = property;
			}
		}
	}
}