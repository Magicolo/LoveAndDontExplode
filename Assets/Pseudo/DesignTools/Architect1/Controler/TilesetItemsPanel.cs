﻿using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using Pseudo.Injection;

namespace Pseudo.Architect
{
	public class TilesetItemsPanel : MonoBehaviour
	{
		[Inject()]
		ArchitectControler Architect = null;
		[Inject()]
		ArchitectBehavior ArchitectBehavior = null;
		//[Inject()]
		//DrawingControler DrawingControler = null;
		[Inject()]
		ArchitectToolControler ToolControler = null;
		[Inject()]
		ArchitectLinker Linker = null;

		List<Button> tilesetButtons = new List<Button>();
		TileSet selectedTileset;
		int selectedTileIndex;

		Color SelectedColor { get { return ArchitectBehavior.Skin.SelectedButtonBackground; } }
		Color BaseColor { get { return ArchitectBehavior.Skin.EnabledButtonBackground; } }


		void Start()
		{
		}

		void Update()
		{
			Refresh();
		}

		public void Refresh()
		{
			TileSet tileset = Linker.Tilesets[0];
			if (Architect.MapLoaded && selectedTileset != tileset)
			{
				clearTilesetButtons();
				selectedTileset = tileset;
				showTileset();
				selectTile(0);
			}
		}

		void showTileset()
		{
			for (int i = 0; i < selectedTileset.Tiles.Count; i++)
			{
				TileType tileType = selectedTileset[i];
				int index = i; // Fix for a compiler strange behavior. bouttonClicked would have all had the same value without this realocation
				UnityAction action = () => buttonClicked(index);
				Button button = ArchitectBehavior.UIFactory.CreateImageButton(transform, tileType.PreviewSprite, BaseColor, action);

				tilesetButtons.Add(button);
			}
		}

		void buttonClicked(int index)
		{
			ToolControler.SelectedTileType = selectedTileset.Tiles[index];
			selectTile(index);
		}

		private void clearTilesetButtons()
		{
			for (int i = 0; i < tilesetButtons.Count; i++)
			{
				tilesetButtons[i].gameObject.Destroy();
			}
			tilesetButtons.Clear();
		}

		private void selectTile(int index)
		{
			if (selectedTileIndex >= 0)
				tilesetButtons[selectedTileIndex].GetComponent<Image>().color = BaseColor;

			selectedTileIndex = index;

			if (selectedTileIndex >= 0)
				tilesetButtons[selectedTileIndex].GetComponent<Image>().color = SelectedColor;

		}
	}
}