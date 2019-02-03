using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
using Objects;
namespace Objects.Galaxy
{
    public class StarFactory : MonoBehaviour
    {
        [SerializeField] public GameObject[] _sceneToPrefab;
        [SerializeField] public StarConnectionFactory starConnectionFactory;
        [SerializeField] public PlanetFactory planetfactory;
        public Sprite[] starIconSprites;
        public void Start(){
            starIconSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star");
            if (starIconSprites == null){
                Debug.LogError("failed to load StarIcon Sprites");
            }
        }
        public StarNode createStar(Transform holder, StarNodeModel model){
            var star = createBaseStar();
            initBaseStar(star,holder,model.position,model.id);
            star.name = model.name;
            star.gameObject.name = model.name;
            foreach(var connection in model.starConnections){
                var otherId = model.id == connection.starIds[0]? connection.starIds[1] : connection.starIds[0];
                var starRef = new Reference<StarNode>(otherId);
                makeConnection(star,starRef);
            }
            var planets = new Reference<Planet>[model.planets.Length];
            var count = 0;
            foreach(var planetModel in model.planets){
                planets[count++] = new Reference<Planet>(planetfactory.makePlanet(star,planetModel));
            }
            star.setPlanets(planets);
            return star;
        }
        public StarNode createStar(Transform holder, Vector3 position){
            var star = createBaseStar();
            initBaseStar(star,holder,position);
            return star;
        }
        public void initBaseStar(StarNode star, Transform holder, Vector3 position,long id = -1){
            star.transform.SetParent(holder);
            star.transform.position = position;
            var representation = new GameObject("representation");
            representation.transform.SetParent(star.transform.transform);

            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for (int i = 0; i < _sceneToPrefab.Length; i++)
            {
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }
            infos[3].appearPosition = Vector3.zero;
            infos[2].appearPosition = position;
            var mainrep = new MultiSceneAppearer(infos,representation.transform);
            var rep = new LinkedAppearer(mainrep);

            star.Init(rep,starIconSprites[0]);
            star.transform.name = star.name;
            star.stamp = new FactoryStamp("basic star");
            if(id==-1){
                star.id = GameManager.idMaker.newId(star);
            }else{
                star.id = GameManager.idMaker.insertObject(star,id);
            }
        }

        public StarNode createBaseStar()
        {
            var starGo = new GameObject("starNode");
            var star = starGo.AddComponent<StarNode>();
            var planetHolder = new GameObject("children");
            planetHolder.transform.SetParent(starGo.transform);
            starGo.name = star.name;
            return star;
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
        public virtual StarConnection makeConnection(StarNode a, Reference<StarNode> b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



