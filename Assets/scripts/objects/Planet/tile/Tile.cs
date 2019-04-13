using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
using Newtonsoft.Json;
namespace Objects.Galaxy
{
    public class TileState:TerrestrialState{
        public Reference<Building> building = null;
        public int tilePosition;

        public void setBuilding(Building building){
            this.building = (Reference<Building>)building;
        }

    }
	[JsonObject(MemberSerialization.OptIn)]
    public partial class Tile: IContextable, IIconable, IActOnable,ISaveable<TileState>
    {
        public long getId(){
            return state.id;
        }
        public object  stateObject{get{return state;}set{state = (TileState)value;}}

        public TileState state{get;set;}

        // public Tile(Sprite texture, Building building, TileModel model)
        // {
        //     title = "tile";
        //     sprite = texture;
        //     this.tilePosition = model.position;
        //     this.building = building;
        // }
        public Tile(TileState state){
            this.state = state;
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

            if (state.building != null){
                var right = new GameObject("info-right");
                right.transform.SetParent(holder.transform, false);
                layout = right.AddComponent<VerticalLayoutGroup>();
                layout.childControlHeight = false;
                layout.childControlWidth = false;
                layout.childForceExpandHeight = false;
                layout.childForceExpandWidth = false;
                state.building.value.renderIcon(callbacks).transform.SetParent(right.transform,false);
                foreach( var infoObj in state.building.value.renderInfo(callbacks)){
                    infoObj.transform.SetParent(right.transform,false);
                }
            }else{
                Debug.Log("no building");
            }
            return holder; 
        }
        public IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = "tile " +state.tilePosition;
            info.icon = state.sprite;
            return info;
        }
        private GameObject renderSimpleIcon(int width = 20,int height = 20 ){
            var tile = new GameObject("TileIcon");
            tile.AddComponent<TileStub>().tile = this;
            var image = tile.AddComponent<Image>();
            image.sprite = state.sprite;
            if (state.building != null){
                var bInfo = state.building.value.getIconableInfo();
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
            if (state.building != null){
                var buildingIcon =  state.building.value.renderIcon(viewCallBacks);
                var buildingInfo = state.building.value.renderInfo(viewCallBacks);
                buildingInfo.Insert(0,buildingIcon);
                return buildingInfo;
            }else{
                return new List<GameObject>(){ state.building.value.renderIcon(viewCallBacks)};
            }

        }
    }


}
