using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
namespace Objects.Galaxy
{
    public class TileModel{
        public int position;
        public long id;
        public BuildingModel building;
        public TileModel(){}
        public TileModel(Tile tile){
            if (tile.building != null){
                building = tile.building.model;
            }else{
                building = null;
            }
            id = tile.id;
            position = tile.tilePosition;
        }
    }
    public partial class Tile: IContextable, IIconable, IActOnable, ISaveAble<TileModel>
    {
        public TileModel model{get{return new TileModel(this);}}
        public long id;
        public Building building = null;
        public void setBuilding(Building building){
            this.building = building;
        }
        public int tilePosition;
        private Sprite sprite = null;
        public string title{get;}
        public Tile(Sprite texture, int tilePosition)
        {
            title = "tile";
            sprite = texture;
            this.tilePosition = tilePosition;
        }
        public Tile(Sprite sprite, Building building, int tilePosition):this(sprite,tilePosition){
            this.building = building;
        }

    }

    public partial class Tile{
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
            var holder =  new GameObject("TileContext");
            holder.transform.SetParent(parent, false);
            var layout = holder.AddComponent<VerticalLayoutGroup>();
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            holder.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;
            var thisIcon = renderSimpleIcon();
            thisIcon.transform.SetParent(holder.transform, false);
            thisIcon.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;

            if (building != null){
                var right = new GameObject("info-right");
                right.transform.SetParent(holder.transform, false);
                layout = right.AddComponent<VerticalLayoutGroup>();
                layout.childControlHeight = false;
                layout.childControlWidth = false;
                layout.childForceExpandHeight = false;
                layout.childForceExpandWidth = false;
                building.renderIcon(callbacks).transform.SetParent(right.transform,false);
                foreach( var infoObj in building.renderInfo(callbacks)){
                    infoObj.transform.SetParent(right.transform,false);
                }
            }else{
                Debug.Log("no building");
            }
            return holder; 
        }
        public iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = "tile " +tilePosition;
            info.icon = sprite;
            return info;
        }
        private GameObject renderSimpleIcon(int width = 20,int height = 20 ){
            var tile = new GameObject("TileIcon");
            tile.AddComponent<TileStub>().tile = this;
            var image = tile.AddComponent<Image>();
            image.sprite = sprite;
            if (building != null){
                var bInfo = building.getIconableInfo();
                var topImage = new GameObject("buildingSprite");
                topImage.transform.SetParent(tile.transform);
                topImage.AddComponent<Image>().sprite = bInfo.icon;
                topImage.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            }
            image.rectTransform.sizeDelta = new Vector2(width,height);
            return tile;
        }
        public GameObject renderIcon(clickViews callbacks){
            var tile = renderSimpleIcon();
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
