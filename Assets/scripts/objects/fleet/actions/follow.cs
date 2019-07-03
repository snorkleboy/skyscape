using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]

    public class Follow : StateAction{
        Fleet fleet;
        Fleet targetFleet;
        public Follow init(Fleet fleet, Fleet targetFleet){
            this.fleet = fleet;
            this.targetFleet = targetFleet;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
   
            var shipsMovingBehavior = new IEnumerator[fleet.state.shipsContainer.ships.Count];
            var count = 0;
            foreach(var shipMovable in fleet.state.shipsContainer.ships){
                shipsMovingBehavior[count++] = Galaxy.ship.ShipStateActions.followReference(shipMovable,targetFleet.state.positionState);
            }
            yield return util.Routiner.Any(
                keepIconToAveragePosition(),
                util.Routiner.All(shipsMovingBehavior)
            );
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
                pos += ship.value.state.positionState.position;
            }
            return pos/fleet.state.shipsContainer.ships.Count;
        }
    }

}

