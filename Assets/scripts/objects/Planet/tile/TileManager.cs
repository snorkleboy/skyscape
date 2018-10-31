using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
namespace Objects.Galaxy
{
    [Serializable]
    public class TileManager: IViewable{
        public int updateId{get;}
        [SerializeField]public Tile[] tiles;
        public TilerView tilerView = new TilerView();
        public int height;
        public int width;
        public TileManager(int width, int height, Tile[] tiles){
            this.height = height;
            this.width = width;
            this.tiles = tiles;
        }
        public GameObject renderUIView(Transform parent,clickViews callbacks){
            Debug.Log("Tilemanger render UIVIEW");
            return tilerView.render(this,parent,callbacks);
        }
    }
}