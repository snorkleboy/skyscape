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
        public void Init(ShipState state,SingleSceneAppearer renderer){
            this.state = state;
            this.debugState = state;
            appearer = renderer;
        }
        protected override void OnMouseEnter()
        {
            this.state.fleetShipIsIn.OnMouseEnterShip(this);
        }
        protected override void OnMouseExit()
        {
            this.state.fleetShipIsIn.OnMouseExitShip(this);
        }
        protected override void OnMouseDown()
        {
            this.state.fleetShipIsIn.OnMouseDownShip(this);
        }

        public override IconInfo getIconableInfo(){
            return new IconInfo();
        }
    }

}