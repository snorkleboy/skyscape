using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

namespace Objects
{
     [System.Serializable]
    public class MoveFleetBetweenPoints : StateAction
    {
        Fleet fleet;
        Vector3[] targets;
        int targetI;
        public MoveFleetBetweenPoints init(Fleet fleet, Vector3[] targets){
            this.fleet = fleet;
            this.targets = targets;
            this.targetI = 0;
  
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            while(true){
                yield return new MoveFleet().init(this.fleet,targets[targetI]);
                targetI = (targetI + 1)%targets.Length;
            }
        }
        
    }
}