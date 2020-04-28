using System.Collections.Generic;
using Objects.Galaxy;
using Objects.Galaxy.State;
using Newtonsoft.Json;
namespace Objects
{
    [System.Serializable]
    public class ShipsContainer:AppearableContainerState{

        [JsonProperty]public List<Reference<Ship>> ships = new List<Reference<Ship>>();
        public System.Action onEmpty;

        public virtual bool removeShip(Ship ship){
            this.ships.Remove(ship);
            this.appearables.Remove(ship);
            if(this.ships.Count == 0 && onEmpty!= null){
                onEmpty();
            }
            return true;
        }
        public virtual bool addShips(Ship ship){
            this.ships.Add(ship);
            this.appearables.Add(ship);
            return true;
        }
        public virtual bool addShips(List<Ship> ships ){
            this.ships.AddRange(ships.referenceAll());
            this.appearables.AddRange(ships);
            return true;
        }
    }
    [System.Serializable]
    public class DockableShipContainer : ShipsContainer
    {
        [JsonProperty] public int dockSize;
        [JsonProperty] public int dockGridSize;
        [JsonProperty] public List<Reference<Ship>> dockedShips;
    }
}
