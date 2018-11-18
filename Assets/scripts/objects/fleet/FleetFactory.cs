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
        public GameObject[] sceneToPrefab;

        public void Awake(){
            shipFactory = gameObject.AddComponent<ShipFactory>();
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0];
        }
        public Fleet makeFleet(Faction faction, Transform parent){
            var fleetGo = new GameObject("fleet");
            fleetGo.SetParent(parent);
            var fleet = fleetGo.AddComponent<Fleet>();
            var fleetRenderer = new HolderRenderer<Fleet>(sceneToPrefab,parent,fleet);
            fleet.Init("fleet" +  faction.fleets.Count,icon,fleetRenderer);
            faction.fleets[fleet.name] = fleet;
            return fleet;
        }
    }
}