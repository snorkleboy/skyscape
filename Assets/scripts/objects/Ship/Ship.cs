using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
using UI;
namespace Objects.Galaxy
{
    public partial class Ship : MoveAbleGameObject
    {
        public override IMover mover{get{return moverHelper;}}
        private ShipMover moverHelper;
        public void Init(SingleSceneAppearer renderer){
            appearer = renderer;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
            moverHelper = gameObject.AddComponent<ShipMover>();
        }
        public override void appear(int context){
            Debug.Log("ship render " + this);
            appearer.appear(context);
            var pos = appearer.activeGO.transform.position;
            transform.position = pos;
            appearer.activeGO.transform.position = pos;
        }

    }

    public partial class Ship{
        public override iconInfo getIconableInfo(){
            return new iconInfo();
        }
    }

}