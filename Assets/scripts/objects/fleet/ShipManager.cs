using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class ShipManager{
        public FleetMover mover;

        public ShipManager(FleetMover mover){
            this.mover = mover;
        }
        public ShipManager(List<Ship> ships){
            addShips(ships);
        }
        public List<Ship> ships = new List<Ship>();
        public ShipManager addShips(Ship ship){
            this.ships.Add(ship);
            mover.addMover(ship.mover);
            return this;
        }
        public ShipManager addShips(List<Ship> ships ){
            this.ships.AddRange(ships);
            foreach(var ship in ships){
                mover.addMover(ship.mover);
            }
            return this;
        }
    }
}