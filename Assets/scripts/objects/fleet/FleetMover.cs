using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [System.Serializable]
    public class FleetMover : Mover
    {
        private List<IMover> subMovers = new List<IMover>();
        public Transform fleetTransform;
        public MoveFleet moveFleetBehavior;
        public override Vector3 getPosition(){
            return fleetTransform.position;
        }
        public void addMover(IMover mover){
            subMovers.Add(mover);
        }
        public void addMover(IEnumerable<IMover> movers){
            subMovers.AddRange(movers);
        }

        public override StateAction setTarget(UnityEngine.Vector3 target, float d = 0.5f){
            return moveFleetBehavior = new MoveFleet().Init(subMovers, target, fleetTransform);
        }
    }
    [System.Serializable]
    public class MoveFleet : Objects.StateAction{
        List<IMover> subMovers;
        List<StateAction> subActions = new List<StateAction>();
        Vector3 target;
        Transform fleetRepresentationTransform;
        public MoveFleet Init(List<IMover> subMovers, Vector3 target, Transform fleetIcon){
            this.subMovers = subMovers;
            this.target = target;
            this.fleetRepresentationTransform = fleetIcon;
            base.Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            float offset = 0;
            var shipsMovingBehavior = new IEnumerator[subMovers.Count];
            var count = 0;
            var towardsTarget = (target -fleetRepresentationTransform.position );
            var perpendicular = Vector3.Cross(towardsTarget,Vector3.up);
            perpendicular.Normalize();

            foreach(var mover in subMovers){
                var subaction = mover.setTarget(makeOffset(perpendicular,target,count));
                subActions.Add(subaction);
                shipsMovingBehavior[count++] = subaction;
                offset += 1;
            }
            return util.Routiner.Any(
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
                fleetRepresentationTransform.position = getAveragePosition();
                yield return null;
            }
        }
        private Vector3 getAveragePosition(){
            Vector3 pos = Vector3.zero;
            foreach(var mover in subMovers){
                pos += mover.getPosition();
            }
            return pos/subMovers.Count;
        }
    }

}

