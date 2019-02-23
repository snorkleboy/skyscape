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
            position = ship.mover.getPosition();
        }
        public long id;
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
    }
    public partial class Ship : MoveAbleGameObject,ISaveAble<ShipModel>,IIded
    {
        public long id;
        public long getId(){return id;}
        public ShipModel model{get{return new ShipModel(this);}}
        public override IMover mover{get{return moverHelper;}}
        private ShipMover moverHelper;
        public void Init(SingleSceneAppearer renderer){
            appearer = renderer;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
            moverHelper = gameObject.AddComponent<ShipMover>();
        }
        public override void appear(int context){
            if(appearer.appear(context)){
                var pos = appearer.activeGO.transform.position;
                transform.position = pos;
                appearer.activeGO.transform.position = pos;
            }
        }
    }

    public partial class Ship{
        public override iconInfo getIconableInfo(){
            return new iconInfo();
        }
    }

}