using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]

    public class ShipToShipFollow : StateAction{
        Fleet fleet;
        Fleet targetFleet;
        public ShipToShipFollow init(Fleet fleet, Fleet targetFleet){
            this.fleet = fleet;
            this.targetFleet = targetFleet;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            var shipsMovingBehavior = new IEnumerator[fleet.state.shipsContainer.ships.Count];
            var otherShips = targetFleet.state.shipsContainer.ships;

            var count = 0;
            foreach(var shipMovable in fleet.state.shipsContainer.ships){
                var otherShip = otherShips[count % otherShips.Count].value;
                shipsMovingBehavior[count++] = Galaxy.ship.ShipStateActions.followReference(shipMovable,otherShip.state.positionState);
                count += 1;
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
                var mover = ship.value.mover;
                pos += mover.appearableState.position;
            }
            return pos/fleet.state.shipsContainer.ships.Count;
        }
    }

}

