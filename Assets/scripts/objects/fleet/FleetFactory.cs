using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class FleetFactory
    {
        public ShipFactory shipFactory;
        public Sprite icon;
        public void Awake(List<Ship> ships,string name){
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0];
        }
        public Fleet makeFleet(){
            var fleet = new Fleet("a fleet",icon);
            return fleet;
        }
    }
}