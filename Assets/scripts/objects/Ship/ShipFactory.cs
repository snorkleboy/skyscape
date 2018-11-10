using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    public class ShipFactory
    {
        public Sprite[] shipIcons;
        public GameObject[] shipPrefabs;
        public ShipFactory()
        {
            shipPrefabs = AssetSingleton.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ships");
        }
        public Ship makeShip(Transform parent){
            var renderer = new ShipRenderer(shipPrefabs[Random.Range(0,shipPrefabs.Length)]);
            return new Ship(renderer);
        }
    }


}