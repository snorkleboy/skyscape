using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [System.Serializable]
    public class FleetMover : Mover
    {
        public List<IMover> subMovers = new List<IMover>();
        public Transform fleetTransform;
        public Fleet fleet;
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
            return Fleet.FleetStateActions.moveFleet(fleet, target);
        }
    }
    [System.Serializable]
    public class MoveFleetModel :StateActionModel{
        public MoveFleetModel(){}
        public MoveFleetModel(MoveFleet action){
            target = action.target;
            constructorName = nameof(Fleet.FleetStateActions.moveFleet);
        }
        public override Objects.StateAction hydrate<T>(T stateSource){
            Fleet fleet= stateSource as Fleet;
            if (fleet == null){
                Debug.LogError("couldnt cast stateSource to fleet");
                return null;
            }else{
                return new MoveFleet().Init(fleet, target);
            }

        }
        public SerializableVector3 target;

    }
    [System.Serializable]
    public class MoveFleet : Objects.StateAction{
        public override StateActionModel model{get{return new MoveFleetModel(this);}}
        List<IMover> subMovers;
        [SerializeField] List<StateAction> subActions = new List<StateAction>();
        public Vector3 target;
        Transform fleetRepresentationTransform;
        bool tempActive = true;
        Fleet tempFleet;
        public MoveFleet Init(Fleet fleet, Vector3 target){

            this.subMovers = fleet.ships.mover.subMovers;
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

