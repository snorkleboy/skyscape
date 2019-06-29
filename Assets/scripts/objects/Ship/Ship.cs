using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
using UI;
using System.Runtime.Serialization;

namespace Objects.Galaxy
{
    [System.Serializable]
    [DataContract]

    public class ShipState : GalaxyGameObjectState{
        public Fleet fleetShipIsIn;
        [DataMember]public Galaxy.weapon.Weapon[] weapons;
        [DataMember]public Galaxy.State.DestructableState destructableState;
        [DataMember]public Galaxy.State.ShieldedState shieldedState;
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