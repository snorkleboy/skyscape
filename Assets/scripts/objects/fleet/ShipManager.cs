using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class ShipManager{
        public ShipManager(){

        }
        public ShipManager(List<Ship> ships){
            addShips(ships);
        }
        public List<Ship> ships = new List<Ship>();
        public ShipManager addShips(Ship ship){
            this.ships.Add(ship);
            return this;
        }
        public ShipManager addShips(List<Ship> ships ){
            this.ships.AddRange(ships);
            return this;
        }
    }
}