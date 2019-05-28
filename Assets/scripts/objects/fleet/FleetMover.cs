using System.Collections.Generic;
using Objects.Galaxy.State;
using UnityEngine;
namespace Objects
{
    [System.Serializable]
    public class FleetMover:MonoBehaviour,IMover
    {
        public AppearableState appearableState{get{return fleet.state.positionState;}}
        public StateActionState stateActionState{get{return fleet.state.actionState;}}
        public FleetMover init(Fleet fleet){
            this.fleet = fleet;
            return this;
        }
        public Fleet fleet;
        public void moveTo(UnityEngine.Vector3 target, float d = 0.5f){
            fleet.state.actionState.setStateAction(FleetStateActions.moveFleet(fleet, target));
        }
        public void patrol(UnityEngine.Vector3[] targets, float d = 0.5f){
            fleet.state.actionState.setStateAction(FleetStateActions.patrolFleet(fleet, targets));
        }
    }

}

