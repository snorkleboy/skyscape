using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Galaxy.State;

namespace Objects
{
    public class ShipMover:IMover{
        public Ship ship;
        public AppearableState appearableState{get{return ship.state.positionState;}}
        public StateActionState stateActionState{get{return ship.state.actionState;}}
        public ShipMover init(Ship ship){
            this.ship = ship;
            return this;
        }
        public void moveTo(Vector3 target,float d = .5f){
            stateActionState.setStateAction(new Objects.Galaxy.ship.MoveToPoint().Init(appearableState,5f,d,target));
        }

    }
}