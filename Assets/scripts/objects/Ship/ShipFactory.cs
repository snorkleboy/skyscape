using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    public class ShipFactory :MonoBehaviour
    {
        public Sprite[] shipIcons;
        public GameObject[] shipPrefabs;
        public void Awake()
        {
            shipPrefabs = AssetSingleton.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ship");
            if(shipPrefabs == null){
                Debug.LogError("ship factory ship prefab not found");
            }
            shipIcons =new Sprite[]{ AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0]};
            if(shipIcons[0] == null){
                Debug.LogWarning("ship factory did not find icon");
            }
        }
        public Ship makeShip(Fleet fleet){
            var prefab = shipPrefabs[0];
            var ship = fleet.gameObject.AddComponent<Ship>();
            var renderer = new SingleSceneRenderer<Ship>(prefab,3,null,ship);
            ship.Init(renderer);
            fleet.addShips(ship);
            return ship;
        }
    }


}