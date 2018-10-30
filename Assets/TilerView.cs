using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
public class TilerView {

	private Action<IContextable> _tileCB;
	public GameObject render(TileManager tileManager,Transform parent, Action<IContextable> tileCB){
		Debug.Log("rendering tiler view");
		_tileCB = tileCB;
		var Holder = new GameObject("grid");
		Holder.transform.SetParent(parent);
		var grid = Holder.AddComponent<GridLayoutGroup>();

		var rect = Holder.GetComponent<RectTransform>();
		rect.anchorMin = Vector2.zero;
		rect.anchorMax = Vector2.one;

		rect.offsetMax =Vector2.zero;
		rect.offsetMin =Vector2.zero;
		var height = rect.rect.height;
		var width = rect.rect.width;

		var tileDim = height/tileManager.height;
		rect.rect.Set(0,0,tileDim * tileManager.width,tileDim);
		grid.cellSize = new Vector2(tileDim,tileDim);
		Holder.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;
		grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.constraintCount = tileManager.width;
		grid.childAlignment = TextAnchor.UpperCenter;

		foreach (var tile in tileManager.tiles)
		{
			makeTile(Holder.transform,tile);
		}
		return Holder;
	}
	public void makeTile(Transform parent,Tile tileClass){

		var tile = new GameObject("Tile");
		tile.transform.SetParent(parent);
		tile.AddComponent<TileStub>().tile = tileClass;
		var image = tile.AddComponent<Image>();
		image.sprite = tileClass.sprite;

		var button = tile.AddComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(()=>{Debug.Log("clicked" + tileClass);_tileCB(tileClass);});
	}

}
