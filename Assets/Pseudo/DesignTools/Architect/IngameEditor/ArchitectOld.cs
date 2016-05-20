﻿using UnityEngine;
using System.Collections.Generic;
using System;
using Pseudo.UI.Internal;

namespace Pseudo
{
	public class ArchitectOld : MonoBehaviour
	{

		public ArchitectLinker Linker;
		public UIFactory UiFactory;


		public SpriteRenderer PreviewSprite;

		/*done*/public RectTransform drawingRect;
		/*done*/public bool IsMouseInDrawingRegion { get { return RectTransformUtility.RectangleContainsScreenPoint(drawingRect, UnityEngine.Input.mousePosition, UICam); } }


		/*Done*/public Camera Cam;
		/*Done*/public Camera UICam;

		/*Done*/public LayerData SelectedLayer;
		public List<LayerData> Layers = new List<LayerData>();

		/*Done*///ArchitectHistory architectHistory = new ArchitectHistory();
		/*Done*///public bool HasHistory { get { return architectHistory.History.Count > 0; } }
		/*Done*////public bool HasRedoHistory { get { return architectHistory.HistoryRedo.Count > 0; } }


		//public ArchitectRotationFlip RotationFlip { get { return toolControler.RotationFlip; } }

		//ArchitectTilePositionGetter tilePositionGetter = new ArchitectTilePositionGetter(Vector3.zero, null);

		public GridScallerTiller Grid;


		public UISkin UISkin { get; private set; }

		//ArchitectToolControler toolControler;

		string FileName;
		Point2 mapDimension;

		//ArchitectMenus Menu;
		//ToolbarPanel Toolbar;
		//TilesetItemsPanel TilesetPanel;
		//LayerPanel LayerPanel;


		public OpenFileBrowser OpenFileBrowser;

		Transform MapParent;


		/*public ToolFactory.ToolType SelectedToolType
		{
			get { return toolControler.SelectedToolType; }
			set
			{
				toolControler.SelectedToolType = value;
				Toolbar.Refresh();
			}
		}
		public TileType SelectedTileType
		{
			get { return toolControler.SelectedTileType; }
			set
			{
				toolControler.SelectedTileType = value;
				updatePreviewSprite();
				SelectedToolType = ToolFactory.ToolType.Brush;
				//TilesetPanel.Refresh();
			}
		}*/

		void Awake()
		{
			//Menu = GetComponentInChildren<ArchitectMenus>();
			//Toolbar = GetComponentInChildren<ToolbarPanel>();
			//TilesetPanel = GetComponentInChildren<TilesetItemsPanel>();
			//LayerPanel = GetComponentInChildren<LayerPanel>();
			UISkin = GetComponentInChildren<UISkin>();
			//toolControler = GetComponentInChildren<ArchitectToolControler>();
			//toolControler.SelectedTileType = Linker.Tilesets[0].Tiles[0];
		}
		void Start()
		{
			//toolControler.SelectedToolType = ToolFactory.ToolType.Brush;
			New();
		}

		/*private void updatePreviewSprite()
		{
			PreviewSprite.transform.Reset();
			PreviewSprite.enabled = SelectedLayer.IsActive;
			if (!SelectedLayer.IsActive) return;

			if (tilePositionGetter.Valid)
			{
				PreviewSprite.enabled = true;
				if (toolControler.SelectedTileType == null)
					PreviewSprite.sprite = null;
				else
					PreviewSprite.sprite = toolControler.SelectedTileType.PreviewSprite;
				PreviewSprite.transform.Translate(tilePositionGetter.TileWorldPosition);
				toolControler.RotationFlip.ApplyTo(PreviewSprite.transform);
			}
			else
			{
				PreviewSprite.enabled = false;
			}
		}*/

		public void Save()
		{
			SaveWorld.SaveAll(this, Application.dataPath + "/map/" + FileName + ".arc");
		}

		public void ResetGridSize()
		{
			Grid.NbTilesX = SelectedLayer.LayerWidth;
			Grid.NbTilesY = SelectedLayer.LayerHeight;
			Grid.TileWidth = SelectedLayer.TileWidth;
			Grid.TileHeight = SelectedLayer.TileHeight;
		}

		public void Open(string path)
		{
			OpenFileBrowser.gameObject.SetActive(false);
			clearAllLayer();
			var layers = WorldOpener.OpenFile(Linker, path, MapParent);
			mapDimension = new Point2(layers[0].LayerWidth, layers[0].LayerHeight);
			Layers.AddRange(layers);
			SelectedLayer = layers[0];
			//LayerPanel.RefreshUI();
			ResetGridSize();

		}

		public void New()
		{
			clearAllLayer();
			addLayer();
			SelectedLayer = Layers[0];
			ResetGridSize();
			//LayerPanel.RefreshUI();
		}

		public void New(string text, int width, int height)
		{
			FileName = text;
			mapDimension = new Point2(width, height);
			New();
		}

		private void clearAllLayer()
		{
			for (int i = 0; i < Layers.Count; i++)
			{
				Layers[i].LayerTransform.gameObject.Destroy();
			}
			Layers.Clear();
			if (MapParent)
				MapParent.gameObject.Destroy();

			MapParent = new GameObject("Map").transform;
		}

		/*
		void Update()
		{
			UpdateTileGetter();

			if (UnityEngine.Input.GetMouseButton(0))
				HandleLeftMouse();
			else if (UnityEngine.Input.GetMouseButton(1))
				HandlePipette();

			//Menu.Refresh();
		}*/

		/*public void FlipX()
		{
			toolControler.FlipX = !toolControler.FlipX;
			updatePreviewSprite();
		}

		public void FlipY()
		{
			toolControler.FlipY = !toolControler.FlipY;
			updatePreviewSprite();
		}

		public void Rotate()
		{
			toolControler.Rotation -= 90;
			toolControler.Rotation %= 360;
			updatePreviewSprite();
		}*/

		/*private void UpdateTileGetter()
		{
			ArchitectTilePositionGetter newTilePositionGetter = new ArchitectTilePositionGetter(Cam.GetMouseWorldPosition(), SelectedLayer);
			if (newTilePositionGetter.TilePosition != tilePositionGetter.TilePosition)
			{
				tilePositionGetter = newTilePositionGetter;
				updatePreviewSprite();
			}
		}*/


		//done
		/*public void HandleLeftMouse()
		{
			if (IsMouseInDrawingRegion && SelectedLayer.IsInArrayBound(tilePositionGetter.TilePosition) && SelectedLayer.IsActive)
				architectHistory.Do(ToolFactory.Create(toolControler.SelectedToolType, this, tilePositionGetter));
		}

		//done
		public void HandlePipette()
		{
			if (IsMouseInDrawingRegion && SelectedLayer.IsInArrayBound(tilePositionGetter.TilePosition))
				toolControler.SelectedTileType = SelectedLayer[tilePositionGetter.TilePosition].TileType;
		}*/




		public void Undo()
		{
			//architectHistory.Undo();
			//Menu.Refresh();
		}

		public void Redo()
		{
			//architectHistory.Redo();
			//Menu.Refresh();
		}

		/*public void setSelectedTile(int id)
		{
			SelectedTileType = Linker.Tilesets[0].Tiles[id - 1];
		}

		public void AddSelectedTileType(LayerData layer, Vector3 worldP, Point2 tilePoint)
		{
			layer.AddTile(tilePoint, toolControler.SelectedTileType, toolControler.RotationFlip);
		}*/

		/*public void AddTile(LayerData layer, Vector3 worldP, Point2 tilePoint, TileType tileType)
		{
			layer.AddTile(tilePoint, tileType, toolControler.RotationFlip);
		}*/

		public void AddTile(LayerData layer, Vector3 worldP, Point2 tilePoint, TileType tileType, ArchitectRotationFlip RotationFlip)
		{
			layer.AddTile(tilePoint, tileType, RotationFlip);
		}

		public void RemoveSelectedLayer()
		{
			if (SelectedLayer != null)
			{
				SelectedLayer.LayerTransform.gameObject.Destroy();
				Layers.Remove(SelectedLayer);
				SelectedLayer = null;
			}
		}

		public void MoveDownSelectedLayer()
		{
			int selectIndex = Layers.IndexOf(SelectedLayer);
			if (selectIndex == Layers.Count - 1) return;
			Layers.Switch(selectIndex, selectIndex + 1);
			SelectedLayer.LayerTransform.SetSiblingIndex(SelectedLayer.LayerTransform.GetSiblingIndex() + 1);
		}

		public void MoveUpSelectedLayer()
		{
			int selectIndex = Layers.IndexOf(SelectedLayer);
			if (selectIndex == 0) return;
			Layers.Switch(selectIndex, selectIndex - 1);
			SelectedLayer.LayerTransform.SetSiblingIndex(SelectedLayer.LayerTransform.GetSiblingIndex() - 1);
		}

		public void DuplicateSelectedLayer()
		{
			LayerData newLayer = SelectedLayer.Clone();

			Layers.Insert(SelectedIndex, newLayer);
		}

		public void RemoveTile(Point2 tilePoint)
		{
			SelectedLayer.EmptyTile(tilePoint);
		}

		public int SelectedIndex { get { return Layers.IndexOf(SelectedLayer); } }

		public LayerData addLayer()
		{
			return addLayer(MapParent, "Layer", 1, 1);
		}
		LayerData addLayer(Transform parent, string name, int tileHeight, int tileWidth)
		{
			LayerData newLayer = new LayerData(parent, name, mapDimension.X, mapDimension.Y);
			newLayer.TileHeight = tileHeight;
			newLayer.TileWidth = tileWidth;
			Layers.Add(newLayer);
			return newLayer;
		}


	}

}
