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
            sceneToPrefab[3] = AssetSingleton.getAsset<GameObject>(AssetSingleton.bundleNames.prefabs, "fleetGO");//.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ui")
            if(sceneToPrefab[3] == null){
                Debug.LogWarning("fleet factory did not find fleet prefab");
            }
        }
        public Fleet makeFleet(Faction faction, StarNode parent, Vector3 position){
            var fleetGo = new GameObject("fleet");
            var fleetGoShipsHolder = new GameObject("ships");
            fleetGoShipsHolder.SetParent(fleetGo);
            var fleet = fleetGo.AddComponent<Fleet>();
            fleet.id = GameManager.idMaker.newId(fleet);
            var infos = new sceneAppearInfo[sceneToPrefab.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = new sceneAppearInfo(sceneToPrefab[i]);
            }
            infos[3].appearPosition = position;
            var mainrep = new MultiSceneAppearer(infos,fleetGo.transform);
            var fleetRenderer = new LinkedAppearer(mainrep);

            fleet.Init("fleet" +  faction.fleets.Count,icon,fleetRenderer,position);
            faction.fleets[fleet.name] = fleet;
            fleetGo.name = fleet.name;

            shipFactory.makeShip(fleet);
            shipFactory.makeShip(fleet);
            shipFactory.makeShip(fleet);
            parent.enterStar(fleet);
            return fleet;
        }
    }
}