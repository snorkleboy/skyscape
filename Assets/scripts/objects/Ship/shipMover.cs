using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class ShipMover:IMover{
        public Objects.Galaxy.State.AppearableState appearableState{get;set;}
        public StateActionState stateActionState{get;set;}
        public void init(Ship ship){
            appearableState = ship.state.positionState;
            stateActionState = ship.state.actionState;

        }
        public StateAction moveTo(Vector3 target,float d = .5f){
            return stateActionState.stateAction = new Objects.Galaxy.ship.MoveToPoint().Init(appearableState,.5f,d,target);
        }

    }
}