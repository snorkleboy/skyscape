using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects
{
    public class FleetFactory : MonoBehaviour
    {
        public ShipFactory shipFactory;
        public Sprite icon;

        public void Awake(){
            shipFactory = gameObject.AddComponent<ShipFactory>();
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0];
        }
        public Fleet makeFleet(string name){
            var fleet = new Fleet("a fleet",icon);
            return fleet;
        }
    }
}