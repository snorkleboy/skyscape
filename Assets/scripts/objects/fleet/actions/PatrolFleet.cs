using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace Objects
{
     [System.Serializable]
    public class PatrolFleet : StateAction
    {
        Fleet fleet;
        Vector3[] targets;
        List<Fleet> lastFoundFleets;
        MoveFleetBetweenPoints savedMoveTask;
        public System.Func<List<Fleet>,object> onFindFleet;
        public PatrolFleet init(Fleet fleet, Vector3[] targets){
            this.fleet = fleet;
            this.targets = targets;
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
                    yield return onFindFleet(this.lastFoundFleets);
                }
            }
        }
        private static void log(Fleet fleet,List<Fleet> fleets){
            var str = "";
            foreach(var collider in fleets){
                if(collider != null){
                    str = str +collider.state.namedState.name + " " +"{" +collider.transform.position.x + " " + collider.transform.position.y + "}\n";
                }else{
                    str = str + " null \n";
                }
            }
            Debug.Log(fleet.state.namedState.name + " foundDuringPatrol " + fleets.Count + " " + str);
        }
        public static IEnumerator handleFoundFleetsDefault(Fleet fleet,List<Fleet> foundFleets){
            Debug.Log("handleFoundFleets");
            log(fleet,foundFleets);

            if(foundFleets.Count == 1){
                Debug.Log("move towards found fleet " +  foundFleets[0].state.positionState.position);
                yield return util.Routiner.Any(
                    new Follow().init(fleet,foundFleets[0]),
                    new util.WaitRoutine(25)
                );
            }
        }
    }
}