using UnityEngine;
using Objects.Galaxy.State;
using Newtonsoft.Json;
using System.Collections.Generic;
using Objects.Galaxy;
namespace Objects
{
    [System.Serializable]
    public class FleetState:GalaxyGameObjectState{
        [JsonProperty]public ShipsContainer shipsContainer;
        public FleetState(ShipsContainer ships,Sprite icon, long id, FactoryStamp stamp, NamedState namedState,FactionOwnedState factionOwnedState, AppearablePositionState positionState, StateActionState actionState) : 
        base(icon, id, stamp, namedState, positionState,factionOwnedState, actionState)
        {
            this.shipsContainer = ships;
        }
    }

}
