using UnityEngine;
using Objects.Galaxy.State;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]
    public class FleetState:GalaxyGameObjectState{
        [JsonProperty]public ShipsContainer shipsContainer;
        public FleetState(ShipsContainer ships,Sprite icon, long id, FactoryStamp stamp, NamedState namedState,FactionOwnedState factionOwnedState, AppearableState positionState, StateActionState actionState) : 
        base(icon, id, stamp, namedState, positionState,factionOwnedState, actionState)
        {
            this.shipsContainer = ships;
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