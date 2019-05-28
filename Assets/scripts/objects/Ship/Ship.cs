using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
using UI;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class ShipState : GalaxyGameObjectState{
        public Fleet fleetShipIsIn;
        public Galaxy.weapon.Weapon[] weapons;
        public Galaxy.State.DestructableState destructableState;
        public Galaxy.State.ShieldedState shieldedState;
    }
    public partial class Ship : GalaxyGameObject<ShipState>
    {
        public ShipState debugState;
        public IMover mover{get{return moverHelper;}}
        private ShipMover moverHelper;
        public void Init(ShipState state,SingleSceneAppearer renderer,ShipMover mover){
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