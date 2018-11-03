using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
namespace Objects.Galaxy
{
    public class Tile: IContextable, IIconable, IActOnable
    {
        private Building building = null;
        public int updateId{get;}
        public void setBuilding(Building building){
            this.building = building;
        }
        private int tilePosition;
        private Sprite sprite = null;
        public string title{get;}
        public Tile(Sprite texture, int tilePosition)
        {
            title = "tile";
            sprite = texture;
        }
        public Tile(Sprite sprite, Building building, int tilePosition):this(sprite,tilePosition){
            this.building = building;
        }
        public GameObject renderActionView(Transform parent,clickViews callbacks){
            var holder =  new GameObject("Tile ACTIONS");
            holder.transform.SetParent(parent, false);
            var button = new GameObject("tile action");
            var buttonEl = button.AddComponent<Button>();
            buttonEl.onClick.AddListener(()=>Debug.Log("action view click"));
            button.transform.SetParent(holder.transform,false);
            return holder;
        }
        public GameObject renderContext(Transform parent,clickViews callbacks){
            Debug.Log("RENDER TILE CONTEXT");
            var holder =  new GameObject("TileContext");
            holder.transform.SetParent(parent, false);
            holder.AddComponent<HorizontalLayoutGroup>();
            holder.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;
            renderIcon(callbacks).transform.SetParent(holder.transform, false);

            if (building != null){
                var right = new GameObject("info-right");
                right.transform.SetParent(holder.transform, false);
                right.AddComponent<VerticalLayoutGroup>();
                building.renderIcon(callbacks).transform.SetParent(right.transform,false);
                foreach( var infoObj in building.renderInfo(callbacks)){
                    infoObj.transform.SetParent(right.transform,false);
                }
            }else{
                Debug.Log("no buildin");
            }
            return holder; 
        }
        public GameObject renderIcon(){
            var tile = new GameObject("TileIcon");
            tile.AddComponent<TileStub>().tile = this;
            var image = tile.AddComponent<Image>();
            image.sprite = sprite;
            return tile;
        }
        public GameObject renderIcon(clickViews callbacks){
            var tile = renderIcon();
            var button = tile.AddComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(()=>{Debug.Log("clicked" + this);callbacks.contextViewCallback(this);callbacks.actionViewCallBack(this);});
            return tile;
        }
        public List<GameObject> renderInfo(clickViews viewCallBacks)
        {
            if (building != null){
                var buildingIcon =  building.renderIcon(viewCallBacks);
                var buildingInfo = building.renderInfo(viewCallBacks);
                buildingInfo.Insert(0,buildingIcon);
                return buildingInfo;
            }else{
                return new List<GameObject>(){ building.renderIcon(viewCallBacks)};
            }

        }
    }


}
