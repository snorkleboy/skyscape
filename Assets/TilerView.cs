using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;


public class TilerView {
	private float height;
	private float width;
	public void render(TileManager tileManager, Transform parent){
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
		grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.constraintCount = tileManager.width;
		grid.childAlignment = TextAnchor.UpperCenter;
		foreach (var tile in tileManager.tiles)
		{
			makeTile(Holder.transform,tile);
		}
	}
	// public void makeRows(Transform parent,TileManager tileManager){
	// 	for (var j = 0; j < tileManager.height; j++){
	// 		makeRow(j,parent,tileManager);
	// 	}
	// }
	// public void makeRow(int rowCount,Transform parent,TileManager tileManager){
	// 	var tiles = tileManager.tiles;
	// 	var tileHeight = height/tileManager.height;
	// 	var tileWidth = width/tileManager.width;
	// 	var rowObj = new GameObject("row");
	// 	rowObj.transform.parent = parent;
	// 	for(var i = rowCount*tileManager.width; i < (rowCount+1)*tileManager.width; i++){
	// 		makeTile(rowObj.transform, tiles[i],tileWidth,tileHeight );
	// 	}
	// 	var layoutGroupRow = rowObj.AddComponent<HorizontalLayoutGroup>();
	// 	layoutGroupRow.childForceExpandHeight = true;
	// 	layoutGroupRow.childForceExpandWidth = true;
	// 	layoutGroupRow.childControlHeight = true;
	// 	layoutGroupRow.childControlWidth = true;
	// }
	public void makeTile(Transform parent,Tile tileClass){

		var tile = new GameObject("Tile");
		tile.transform.SetParent(parent);

		var image = tile.AddComponent<Image>();
		image.sprite = tileClass.sprite;

		var button = tile.AddComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(()=>Debug.Log("clicked" + tileClass));
	}

}
