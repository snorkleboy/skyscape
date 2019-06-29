using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;
using Objects.Galaxy;
using Objects.Galaxy.ship;
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
                shipsMovingBehavior[lastTargetedI % shipsMovingBehavior.Length] = ShipStateActions
                    .engageShip(shipMovable,otherShip,()=>onDestroyTargetShip(shipMovable));
                lastTargetedI++;
            }
            yield return util.Routiner.All(
                shipsMovingBehavior
            );
            if(targetFleet.state.shipsContainer.ships.Count == 0 ){
                Debug.Log("destroyed fleet" + targetFleet.state.namedState.name);
            }
        }

        public IEnumerator onDestroyTargetShip(Ship ship){
            Debug.Log("destroyed ship picking new");
            if(targetFleet){
                var otherShips = targetFleet.state.shipsContainer.ships.getAllReferenced();
                if(otherShips.Count > 0){
                    return ShipStateActions
                        .engageShip(ship,otherShips[lastTargetedI++ % otherShips.Count],()=>onDestroyTargetShip(ship));
                }else{
                    return null;
                }
            }else{
                Debug.Log("ENGAGE FLEET DESTROYED FLEET?");
                return null;
            }

        }

    }
}