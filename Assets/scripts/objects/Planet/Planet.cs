using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy;
using UnityEngine.UI;
using UI;
using Objects.Conceptuals;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    public class PlanetState :GalaxyGameObjectState{
        public TileableState tileableState;
        public PlanetState(FactionOwnedState factionState,TileableState tileableState,Sprite icon, long id, FactoryStamp stamp, NamedState namedState, AppearableState positionState, StateActionState actionState) :
        base(icon, id, stamp, namedState, positionState,factionState, actionState)
        {
            this.tileableState = tileableState;
        }
    }
    public class PlanetModel{
        public string name;
        public TileModel[] tiles;
        public int tileWidth;
        public long id;
        public SerializableVector3 position;
        public long factionId;
        public PlanetModel(){}
        public PlanetModel(Planet planet){
            tileWidth = planet.tileable.state.width;
            name = planet.name;
            id = planet.state.id;
            position = planet.appearer.state.position;
            var length = planet.tileable.state.tiles.Length;
            tiles = new TileModel[length];
            for(var i =0; i<length;i++){
                tiles[i] = planet.tileable.state.tiles[i].model;
            }
            // factionId = planet.state.factionState.belongsTo.id;
        }

    }
    [System.Serializable]
    public partial class Planet :GalaxyGameObject<PlanetState>,IViewable,IContextable,IActOnable,  ISaveAble<PlanetModel>
    {
        public PlanetModel model{get{return new PlanetModel(this);}}
        public override IAppearer appearer { get;set; }
        public Tileable tileable;
        public void Init(SingleSceneAppearer renderer,Tileable tileable,PlanetState state)
        {
            appearer = renderer;
            this.state = state;
            this.tileable = tileable;
        }
    }
    public partial class Planet{
        public GameObject renderActionView(Transform parent, clickViews callbacks){
            var holder = new GameObject("planet action view");
            var layout = holder.AddComponent<VerticalLayoutGroup>();
            holder.SetParent(parent,false);
            layout.childControlHeight = false;
		    layout.childControlWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;

            var makeShipButton = new GameObject("make Fleet");
            var button = makeShipButton.AddComponent<Button>();
            button.onClick.AddListener(()=>{
                var fleet = GameManager.instance.factions.GetFaction(this).createFleet(this);
                fleet.appearer.appear(2);
                Debug.Log("fleet created: " + fleet);
            });
            var text = makeShipButton.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.text = "make fleet";
            makeShipButton.SetParent(holder,false);
            return holder;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            callbacks.contextViewCallback(this);
            callbacks.actionViewCallBack(this);
            Debug.Log("RENDER PLANET UI VIEW");
            return tileable.renderUIView(parent,callbacks);
        }
        public GameObject renderContext(Transform parent, clickViews callbacks){
            var holder =  new GameObject("PLANET Context");
            holder.transform.SetParent(parent, false);
            var text = new GameObject("planet info");
            var textComp = text.AddComponent<Text>();
            textComp.text = "number of tiles:" + tileable.state.tiles.Length;
            textComp.fontSize = 16;
            textComp.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.transform.SetParent(holder.transform,false);
            return holder;
        }
        public override IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = state.namedState.name;
            info.icon = state.icon;
            return info;
        }
    }

}
