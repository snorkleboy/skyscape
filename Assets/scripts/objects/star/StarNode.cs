using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using UnityEditor;
using System.Linq;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class StarNodeState:GalaxyGameObjectState{
        public StarAsContainerState asContainerState;

    }
    public class StarNodeModel{
        public SerializableVector3 position;
        FactoryStamp stamp;
        public long id;
        public string name;
        public StarConnectionModel[] starConnections;
        public PlanetModel[] planets;
        public FleetModel[] fleets;
        public StarNodeModel(){}
        public StarNodeModel(StarNode node){
            position = node.transform.position;
            stamp = node.state.stamp;
            id = node.state.id;
            name=node.name;
            var numConn = node.state.asContainerState.connections.Count;
            starConnections = new StarConnectionModel[numConn];
            for(var i =0;i<numConn;i++)
            {
                starConnections[i] = node.state.asContainerState.connections[i].model;
            }

            var planetNum = node.state.asContainerState.planets.Count;
            planets = new PlanetModel[planetNum];
            for(var i =0; i<planetNum;i++){
                planets[i] = node.state.asContainerState.planets[i].value.model;
            }
            fleets = new FleetModel[node.state.asContainerState.fleets.Count];
            for(var i =0;i<node.state.asContainerState.fleets.Count;i++){
                fleets[i] = new FleetModel(node.state.asContainerState.fleets[i].value);
            }
        }
    }

    public partial class StarNode : GalaxyGameObject<StarNodeState>,  ISaveAble<StarNodeModel>
    {
        public StarNodeModel model{get{return new StarNodeModel(this);}}
        [SerializeField]private StarNodeState stateForDebug;
        public void Init(LinkedAppearer renderer,StarNodeState state)
        {
            this.state = state;
            stateForDebug = state;
            this.appearer = renderer;
            this.enterable = new EnterableStar(state.asContainerState);
            this.gameObject.name = state.namedState.name;
        }
        public override IAppearer appearer { get;set; }
        public EnterableStar enterable;
    }
    public partial class StarNode{
        public GameObject renderIcon(){
            var star = new GameObject("StarIcon");
            var image = star.AddComponent<Image>();
            image.sprite = state.icon;
            return star;
        }
        public override IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = name;
            info.icon = state.icon;
            var details = new IconInfo[2];
            var detail = new IconInfo();
            detail.name = state.asContainerState.planets.Count.ToString();
            var bundle = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites];
            var asset = bundle.LoadAsset<Sprite>("43");
            detail.icon = asset;
            details[0] = detail;

            var otherDetail = new IconInfo();
            var popNum = 0;
            foreach(var planet in state.asContainerState.planets){
               foreach(var tile in  planet.value.tileable.state.tiles){
                   if (tile.building != null){
                       if (tile.building.pops != null){
                           popNum += tile.building.pops.Count;
                       }
                   }
               }
            }
            otherDetail.name = popNum.ToString();
            otherDetail.icon =  AssetSingleton.bundles[AssetSingleton.bundleNames.sprites].LoadAsset<Sprite>("69");
            details[1] = otherDetail;

            info.details = details;
            return info;
        }
    }
}
