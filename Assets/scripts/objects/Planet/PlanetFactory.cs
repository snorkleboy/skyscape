using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class PlanetFactory: MonoBehaviour
    {
        public GameObject baseStarFab;
        public TileFactory tileFactory;
        public Sprite[] planetSprites;

        public void Start(){
            planetSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"planet");
        }

        public Planet newPlanet(StarNode star, Vector3 position)
        {
            var parent = new GameObject("planet");
            var planetHolder = star.gameObject.transform.Find("planetHolder");
            parent.SetParent(planetHolder,false);
            var planet = parent.AddComponent<Planet>();
            var rep = new SingleSceneAppearer(new sceneAppearInfo(baseStarFab,position),3,parent.transform);

            var sprite = planetSprites[Random.Range(0,planetSprites.Length-1)];
            planet.Init(rep,sprite);
            planet.tileManager = tileFactory.makeTileManager();
            planet.id = GameManager.idMaker.newId(planet);
            planet.position = position;
            return planet;
        }

    }

}
