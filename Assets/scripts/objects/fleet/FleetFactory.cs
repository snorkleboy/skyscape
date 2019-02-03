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
        private Fleet makeFleet(Faction faction, Vector3 position,string name, long id =-1){
            var fleetGo = new GameObject("fleet");
            var fleetGoShipsHolder = new GameObject("ships");
            fleetGoShipsHolder.SetParent(fleetGo);
            var fleet = fleetGo.AddComponent<Fleet>();
            if(id == -1){
                fleet.id = GameManager.idMaker.newId(fleet);
            }else{
                fleet.id = GameManager.idMaker.insertObject(fleet,id);
            }
            var infos = new sceneAppearInfo[sceneToPrefab.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = new sceneAppearInfo(sceneToPrefab[i]);
            }
            infos[3].appearPosition = position;
            // var mainrep = new MultiSceneAppearer(infos,fleetGo.transform);
            var mainRep = new SingleSceneAppearer( infos[3],3,fleetGo.transform);
            var fleetRenderer = new LinkedAppearer(mainRep);

            fleet.Init(name,icon,fleetRenderer,position, faction);
            faction.fleets[fleet.name] = fleet;
            fleetGo.name = fleet.name;
            return fleet;
        }
        public Fleet makeFleet(Faction faction, StarNode parent, Vector3 position){
            var fleet = makeFleet(faction,position,"fleet" +  faction.fleets.Count);
            shipFactory.makeShip(fleet);
            shipFactory.makeShip(fleet);
            shipFactory.makeShip(fleet);
            parent.fleetEnter(fleet);
            return fleet;
        }
        public Fleet makeFleet(Faction faction, StarNode parent, FleetModel model){
            if (faction.id != model.factionId){
                Debug.LogError("faction id does not match owningFaction id. Model Id:"+model.id + "  model factionId:"+model.factionId);
            }
            var fleet = makeFleet(faction,model.position,model.name,model.id);
            foreach(var shipModel in model.shipModels){
                var ship = shipFactory.makeShip(fleet,shipModel);
            }
            parent.fleetEnter(fleet);
            return fleet;
        }
    }
}