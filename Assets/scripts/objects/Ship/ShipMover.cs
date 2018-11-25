using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ShipMover : Mover
    {
        private List<IMover> subMovers = new List<IMover>();
        private IMover fleetMover;
        public void Init(IMover fleetMover){
            this.fleetMover = fleetMover;
        }
        public void addMover(IMover mover){
            subMovers.Add(mover);
        }
        public void addMover(IEnumerable<IMover> movers){
            subMovers.AddRange(movers);
        }

        public override void setTarget(UnityEngine.Vector3 target, float d = 0.5f){
            float offset = 0;
            foreach(var mover in subMovers){
                mover.setTarget(target,offset);
                offset += 1;
            }
        }
    }
}