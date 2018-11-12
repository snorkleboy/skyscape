using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class Fleet
    {
        public ShipManager ships= new ShipManager();
        public Pop admiral;
        public string name;

        public Sprite icon;
        public Fleet(string name, Sprite icon){
            this.name = name;
            this.icon = icon;
        }
        public Fleet(string name,Sprite icon,List<Ship> ships):this(name, icon){
            this.ships.addShips(ships);
        }

    }

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