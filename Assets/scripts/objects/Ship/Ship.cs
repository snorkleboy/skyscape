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
        private Mover moverHelper;
        public void Init(SingleSceneRenderer<Ship> renderer){
            renderHelper = renderer;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
            moverHelper = gameObject.AddComponent<Mover>();
        }
        public override void render(int context){
            renderHelper.render(context);
        }

    }

    public partial class Ship{
        public override iconInfo getIconableInfo(){
            return new iconInfo();
        }
    }

}