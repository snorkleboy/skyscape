using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [Serializable]
    public class TileManager{
        [SerializeField]public Tile[] tiles;
        public TilerView tilerView = new TilerView();
        public int height;
        public int width;
        public TileManager(int width, int height, Tile[] tiles){
            this.height = height;
            this.width = width;
            this.tiles = tiles;
        }
        public void renderTileView(Transform parent){
            tilerView.render(this, parent);
        }
    }
}