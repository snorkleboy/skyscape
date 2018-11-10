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
        public List<Ship> ships = new List<Ship>();
        public Pop admiral;
        public string name;

        public Sprite icon;
        public Fleet(List<Ship> ships,string name){
            this.ships.AddRange(ships);
            this.name = name;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
        }
    }
}