using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
using UI;
namespace Objects.Galaxy
{
    public partial class Ship : GalaxyGameObject<GalaxyGameObjectState>
    {
        public GalaxyGameObjectState debugState;
        public IMover mover{get{return moverHelper;}}
        private ShipMover moverHelper;
        public void Init(GalaxyGameObjectState state,SingleSceneAppearer renderer,ShipMover mover){
            this.state = state;
            this.debugState = state;
            appearer = renderer;
            moverHelper = mover;
        }
        public override IconInfo getIconableInfo(){
            return new IconInfo();
        }
    }

}