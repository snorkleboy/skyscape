using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Objects.Galaxy.State;
namespace Objects
{
  
    public class ScanNearbyFleets:StateAction
    {
        public float waitTime = .15f;
        public bool shouldExitOnFind = false;

        GalaxyGameObjectState state;
        int radius;
        System.Func<List<Fleet>,object> onFindColliders;
        public ScanNearbyFleets config(System.Action<ScanNearbyFleets> action){
            action(this);
            return this;
        }
        public ScanNearbyFleets init(GalaxyGameObjectState state,int radius,System.Func<List<Fleet>,object> onFindColliders){
            this.radius = radius;
            this.state = state;
            this.onFindColliders = onFindColliders;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            var loop = true;
            while(loop){
                yield return new util.WaitRoutine(waitTime);
                List<Fleet> nearby = null;
                // colliders = Physics.OverlapSphere(appearbleState.position, radius);

                var possibles = state.positionState.starAt.value.state.asContainerState.fleets;
                foreach (var fleet in possibles)
                {
                    nearby = new List<Fleet>();
                    if(fleet.id != state.id){
                        float dist = Vector3.Distance(state.positionState.position, fleet.value.state.positionState.position);
                        if(dist < radius){
                            nearby.Add(fleet);
                        }
                    }
                }
                if(nearby != null && nearby.Count > 0){
                    if(shouldExitOnFind){
                        loop = false;
                    }
                    yield return this.onFindColliders(nearby);
                }
            }
        }



    }
}