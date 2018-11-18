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
                Debug.LogError("ship prefab not found");
            }
        }
        public Ship makeShip(){
            var prefab = shipPrefabs[0];
            var ship =  new Ship();
            var renderer = new SingleSceneRenderer<Ship>(prefab,3,ship);
            ship.Init(renderer);

            return ship;
        }
    }


}