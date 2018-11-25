using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class FleetMover : Mover
    {
        private Vector3 averagePosition;
        private List<IMover> subMovers = new List<IMover>();
        private IMover fleetMover;
        public override Vector3 getPosition(){return averagePosition;}

        public void Init(IMover fleetMover){
            this.fleetMover = fleetMover;
        }
        public void addMover(IMover mover){
            subMovers.Add(mover);
        }
        public void addMover(IEnumerable<IMover> movers){
            subMovers.AddRange(movers);
        }
        public override void Update(){
            averagePosition = setAveragePosition();
        }
        private Vector3 setAveragePosition(){
            Vector3 pos = Vector3.zero;
            foreach(var mover in subMovers){
                pos += mover.getPosition();
            }
            return pos/subMovers.Count;
        }
        public override void setTarget(UnityEngine.Vector3 target, float d = 0.5f){
            float offset = 0;
            foreach(var mover in subMovers){
                mover.setTarget(target + new Vector3(offset,0,0));
                offset += 1;
            }
        }
    }
}