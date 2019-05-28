using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace Objects
{
     [System.Serializable]
    public class EngageFleet : StateAction
    {
        Fleet fleet;
        Fleet targetFleet;
        public int lastTargetedI = 0;

        public EngageFleet init(Fleet fleet, Fleet targetFleet){
            this.fleet = fleet;
            this.targetFleet = targetFleet;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            var shipsMovingBehavior = new IEnumerator[fleet.state.shipsContainer.ships.Count];
            var otherShips = targetFleet.state.shipsContainer.ships.getAllReferenced();
            Debug.Log("engaging fleet with ship count = "+targetFleet.state.shipsContainer.ships.Count);
            foreach(var shipMovable in fleet.state.shipsContainer.ships){
                var otherShip = otherShips[lastTargetedI % otherShips.Count];
                shipsMovingBehavior[lastTargetedI] = targetShip(shipMovable,otherShip,otherShips);
                lastTargetedI++;
            }
            yield return util.Routiner.All(
                shipsMovingBehavior
            );
            if(targetFleet.state.shipsContainer.ships.Count == 0 ){
                Debug.Log("destroyed fleet" + targetFleet.state.namedState.name);
            }
        }

        public IEnumerator targetShip(Galaxy.Ship ship,Galaxy.Ship shipTarget, List<Galaxy.Ship> otherTargets){
            return Galaxy.ship.ShipStateActions.engageShip(ship, shipTarget,()=>onDestroyTargetShip(ship,shipTarget,otherTargets));
        }
        public IEnumerator onDestroyTargetShip(Galaxy.Ship ship,Galaxy.Ship shipTarget, List<Galaxy.Ship> otherTargets){
            otherTargets.Remove(shipTarget);
            Debug.Log("destroyed ship ");
            UnityEngine.MonoBehaviour.Destroy(shipTarget.gameObject);
            if(otherTargets.Count > 0){
                return targetShip(ship,otherTargets[lastTargetedI % otherTargets.Count],otherTargets);
            }else{
                return null;
            }
        }

    }
}