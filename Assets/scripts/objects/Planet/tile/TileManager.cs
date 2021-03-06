using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
namespace Objects.Galaxy
{
    [Serializable]
    public class TileableState{
        public int height;
        public int width;
        [SerializeField]public Reference<Tile>[] tiles;

    }
    public interface ITileable{
        TileableState state{get;}
    }
    [Serializable]
    public class Tileable:ITileable{
        public TileableState state{get;set;}
        public int updateId{get;}
        public TilerView tilerView = new TilerView();
        public Tileable(TileableState state){
            this.state = state;
        }
        public GameObject renderUIView(Transform parent,clickViews callbacks){
            Debug.Log("Tilemanger render UIVIEW");
            return tilerView.render(this,parent,callbacks);
        }
    }
}