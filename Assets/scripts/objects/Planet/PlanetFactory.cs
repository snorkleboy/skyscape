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
        public Planet makePlanet(StarNode star, PlanetModel model){
            var planet = makePlanet(star,model.position,model);
            return planet;
        }
        public Planet makePlanet(StarNode star, Vector3 position,PlanetModel model = null)
        {
            var parent = new GameObject("planet");
            var planetHolder = star.gameObject.transform.Find("planetHolder");
            parent.SetParent(planetHolder,false);
            var planet = parent.AddComponent<Planet>();
            var rep = new SingleSceneAppearer(new sceneAppearInfo(baseStarFab,position),3,parent.transform);

            var sprite = planetSprites[Random.Range(0,planetSprites.Length-1)];
            planet.Init(rep,sprite,model);
            planet.tileManager = tileFactory.makeTileManager();
            planet.position = position;
            if(model == null){
                planet.id = GameManager.idMaker.newId(planet);
            }else{
                planet.id = GameManager.idMaker.insertObject(planet,model.id);
            }

            return planet;
        }

    }

}
