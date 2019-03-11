using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [System.Serializable]
    public class FleetMover : BasicMover
    {
        public FleetMover init(Fleet fleet){
            this.fleet = fleet;
            return this;
        }
        public Fleet fleet;
        public override StateAction moveTo(UnityEngine.Vector3 target, float d = 0.5f){
            var action = FleetStateActions.moveFleet(fleet, target);
            stateActionState.setStateAction(action);
            return action;
        }
    }
    // [System.Serializable]
    // public class MoveFleetModel :StateActionModel{
    //     public MoveFleetModel(){}
    //     public MoveFleetModel(MoveFleet action){
    //         target = action.target;
    //         constructorName = nameof(Fleet.FleetStateActions.moveFleet);
    //     }
    //     public override Objects.StateAction hydrate<T>(T stateSource){
    //         Fleet fleet= stateSource as Fleet;
    //         if (fleet == null){
    //             Debug.LogError("couldnt cast stateSource to fleet");
    //             return null;
    //         }else{
    //             return new MoveFleet().Init(fleet, target);
    //         }

    //     }
    //     public SerializableVector3 target;

    // }
    public static class FleetStateActions{
            public static StateAction moveFleet(Fleet fleet,Vector3 target){
                return new MoveFleet().Init(fleet, target);
            }
    }
    [System.Serializable]
    public class MoveFleet : Objects.StateAction{
        public Vector3 target;
        Fleet fleet;
        bool tempActive = true;
        public MoveFleet Init(Fleet fleet, Vector3 target){

            // this.subMovers = fleet.ships.mover.subMovers;
            this.fleet = fleet;
            this.target = target;
            // if(fleet.appearer.isActive){
                // this.fleetRepresentationTransform = fleet.appearer.activeGO.transform;
            // }else{
                // tempActive = false; 
                // tempFleet = fleet;
            // }
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            while(!tempActive){
                Debug.Log("is active check");
                yield return util.Routiner.wait(2);
                // if(tempFleet.appearer.isActive){
                    // Debug.Log("active");
                    // tempActive = true;
                    // Init(tempFleet,target);
                // }
            }
            float offset = 0;
            var shipsMovingBehavior = new IEnumerator[fleet.state.shipsContainer.ships.Count];
            var count = 0;
            var towardsTarget = (target -fleet.state.positionState.position );
            var perpendicular = Vector3.Cross(towardsTarget,Vector3.up);
            perpendicular.Normalize();

            foreach(var ship in fleet.state.shipsContainer.ships){
                var mover = ship.mover;
                 mover.moveTo(makeOffset(perpendicular,target,count));
                shipsMovingBehavior[count++] = ship.state.actionState.stateAction;
                offset += 1;
            }
            yield return util.Routiner.Any(
                keepIconToAveragePosition(),
                timedOutMove(shipsMovingBehavior,util.Routiner.wait(15))
            );
        }
        protected IEnumerator timedOutMove(IEnumerator[] routines, IEnumerator waiter){
            return util.Routiner.Any(
                util.Routiner.All(routines),
                waiter
            );
            
        }
        protected Vector3 makeOffset(Vector3 perpendicular,Vector3 target, int count){
            return target + perpendicular*3*count;
        }
        protected IEnumerator keepIconToAveragePosition(){
            while(true){
                fleet.state.positionState.position = getAveragePosition();
                yield return null;
            }
        }
        private Vector3 getAveragePosition(){
            Vector3 pos = Vector3.zero;
            foreach(var ship in fleet.state.shipsContainer.ships){
                var mover = ship.mover;
                pos += mover.appearableState.position;
            }
            return pos/fleet.state.shipsContainer.ships.Count;
        }
    }

}

