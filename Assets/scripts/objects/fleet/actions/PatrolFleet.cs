using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace Objects
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class PatrolFleet : SaveableStateAction
    {
        Fleet fleet;
        [JsonProperty]public SerializableVector3[] targets;
        List<Fleet> lastFoundFleets;
        MoveFleetBetweenPoints savedMoveTask;
        public System.Func<List<Fleet>,Fleet,object> onFindFleet;
        public override StateAction hydrate<T>(T source){
            fleet = tryCoerce<T,Fleet>(source);
            return fleet.patrol(Array.ConvertAll(targets,(i)=>(Vector3)i));
        }
        public PatrolFleet init(Fleet fleet, Vector3[] targets){
            this.fleet = fleet;
            this.targets = new SerializableVector3[targets.Length];
            for(var i =0;i<targets.Length;i++){
                this.targets[i] = targets[i];
            }
            savedMoveTask = new MoveFleetBetweenPoints().init(this.fleet,targets);
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            while(true){
                yield return util.Routiner.Any(
                savedMoveTask,
                new ScanNearbyFleets()
                    .config(scanTask=>scanTask.shouldExitOnFind = true)
                    .init(fleet.state,25,(foundFleets)=>{
                        this.lastFoundFleets=foundFleets;
                        return null;
                    })
                );
                Debug.Log("exit any(move,scan)");
                if(onFindFleet != null){
                    Debug.Log("onFindFleet any(move,scan)");
                    yield return onFindFleet(this.lastFoundFleets,this.fleet);
                }
            }
        }
    }
}