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
        public GameObject[] sceneToPrefab = new GameObject[4];

        public void Awake(){
            shipFactory = gameObject.AddComponent<ShipFactory>();
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0];
            if(icon == null){
                Debug.LogWarning("fleet factory did not find icon");
            }
            sceneToPrefab[3] = AssetSingleton.getAsset<GameObject>(AssetSingleton.bundleNames.prefabs, "IconedGO");//.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ui")
            if(sceneToPrefab[3] == null){
                Debug.LogWarning("fleet factory did not find icon");
            }
        }
        public Fleet makeFleet(Faction faction, Transform parent, Vector3 position){
            var fleetGo = new GameObject("fleet");
            fleetGo.SetParent(parent);
            var fleet = fleetGo.AddComponent<Fleet>();
            var fleetRenderer = new HolderRenderer<Fleet>(sceneToPrefab,fleetGo.transform,fleet);
            fleetRenderer.position = position;
            fleet.Init("fleet" +  faction.fleets.Count,icon,fleetRenderer);
            faction.fleets[fleet.name] = fleet;
            fleetGo.name = fleet.name;
            return fleet;
        }
    }
}