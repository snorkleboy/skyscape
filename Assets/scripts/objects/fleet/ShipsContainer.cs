using System.Collections.Generic;
using Objects.Galaxy;
using Objects.Galaxy.State;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]
    public class ShipsContainer:AppearableContainerState{
        [JsonProperty]public List<Reference<Ship>> ships = new List<Reference<Ship>>();
        public void addShips(Ship ship){
            this.ships.Add(ship);
            this.appearables.Add(ship);
        }
        public void addShips(List<Ship> ships ){
            this.ships.AddRange(ships.referenceAll());
            this.appearables.AddRange(ships);
        }
    }
}

                
            // this.name = name;
            // this.state.icon = icon;
            // this.fleetPosition = position;
            // this._appearer = renderHelper;
            // var fleetMover = gameObject.AddComponent<FleetMover>();
            // ships = new ShipManager(fleetMover);
            // owningFaction = faction;

            
        // public void appear(int scene){
            // foreach (var appearable in _appearer.appearables){
            //     var appearPos = fleetPosition + new Vector3(1 + 3*count++,0,0);
            //     appearable.appearer.setAppearPosition(appearPos,3);
            // }
            // if(appearer.appear(scene)){
            //     appearer.activeGO.transform.position = fleetPosition;
            //     this.ships.mover.fleetTransform = appearer.activeGO.transform;
            // }
        // }