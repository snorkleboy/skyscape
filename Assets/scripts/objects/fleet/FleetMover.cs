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
            var enumList = new IEnumerator[subMovers.Count+2];
            var count = 0;
            enumList[count++] = util.Routiner.wait(10000);
            enumList[count++] = keepIconToAveragePosition();
            foreach(var mover in subMovers){
                var beh = mover.setTarget(target + new Vector3(offset,0,0));
                subActions.Add(beh);
                enumList[count++] = beh;
                offset += 1;
            }
            
            return util.Routiner.All(enumList);
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

