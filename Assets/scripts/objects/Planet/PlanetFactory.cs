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

        public Planet newPlanet(Transform holder)
        {
            var rep = new PlanetRenderer(baseStarFab);
            rep.parent = holder;
            Planet planet = new Planet(rep,planetSprites[Random.Range(0,planetSprites.Length-1)], GameManager.instance.shipFactory);
            planet.tileManager = tileFactory.makeTileManager();
            return planet;
        }

    }

}
