using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]

    public class MoveFleet : SaveableStateAction{
        [JsonProperty]public SerializableVector3 target;
        Fleet fleet;
        bool tempActive = true;
        public override StateAction hydrate<T>(T source){
            this.fleet = tryCoerce<T,Fleet>(source);
            base._Init();
            return this;
        }

        public MoveFleet init(Fleet fleet, Vector3 target){
            this.fleet = fleet;
            this.target = target;

            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
   
            float offset = 0;
            var shipsMovingBehavior = new IEnumerator[fleet.state.shipsContainer.ships.Count];
            var count = 0;
            var towardsTarget = (target -fleet.state.positionState.position );
            var perpendicular = Vector3.Cross(towardsTarget,Vector3.up);
            perpendicular.Normalize();

            foreach(var ship in fleet.state.shipsContainer.ships){
                
                var mover = ship.value.mover;
                mover.moveTo(makeOffset(perpendicular,target,count));
                shipsMovingBehavior[count++] = ship.value.state.actionState.stateAction;
                offset += 1;
            }
            yield return util.Routiner.Any(
                keepIconToAveragePosition(),
                timedOutMove(shipsMovingBehavior,util.Routiner.wait(25))
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
                var mover = ship.value.mover;
                pos += mover.appearableState.position;
            }
            return pos/fleet.state.shipsContainer.ships.Count;
        }
    }

}

