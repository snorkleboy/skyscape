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

        public Planet newPlanet(StarNode star)
        {
            var rep = new PlanetRenderer(baseStarFab);
            var parent = new GameObject("planet");
            var planetHolder = star.gameObject.transform.Find("planetHolder");
            parent.transform.SetParent(planetHolder);

            rep.parent = parent.transform;
            var sprite = planetSprites[Random.Range(0,planetSprites.Length-1)];
            var planet = parent.AddComponent<Planet>();
            planet.Init(rep,sprite);
            planet.tileManager = tileFactory.makeTileManager();
            return planet;
        }

    }

}
