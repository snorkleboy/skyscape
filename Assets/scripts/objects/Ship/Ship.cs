using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
using UI;
namespace Objects.Galaxy
{
    public class ShipModel{
        public ShipModel(){}
        public ShipModel(Ship ship){
            id=ship.id;
            position = ship.mover.appearableState.position;
        }
        public long id;
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
    }

    public partial class Ship : GalaxyGameObject<GalaxyGameObjectState>,ISaveAble<ShipModel>
    {
        public long id;
        public ShipModel model{get{return new ShipModel(this);}}
        public IMover mover{get{return moverHelper;}}
        private ShipMover moverHelper;
        public void Init(SingleSceneAppearer renderer){
        //     appearer = renderer;
        //     icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
        //     moverHelper = gameObject.AddComponent<ShipMover>();
        }
        public void appear(int context){
        //     if(appearer.appear(context)){
        //         var pos = appearer.activeGO.transform.position;
        //         transform.position = pos;
        //         appearer.activeGO.transform.position = pos;
        //     }
        }
    }

    public partial class Ship{
        public override IconInfo getIconableInfo(){
            return new IconInfo();
        }
    }

}