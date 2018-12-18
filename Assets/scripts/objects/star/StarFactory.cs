using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
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
        public StarNode createStar(Transform holder, Vector3 position){
            var star = createBaseStar();
            initBaseStar(star,holder,position);
            return star;
        }
        public void initBaseStar(StarNode star, Transform holder, Vector3 position){
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
            var rep = new HolderAppearer(mainrep);

            star.Init(rep,starIconSprites[0]);
            star.transform.name = star.name;
            star.stamp = new FactoryStamp("basic star");
            star.id = GameManager.idMaker.newId(star);
        }

        public StarNode createBaseStar()
        {
            var starGo = new GameObject("starNode");
            var star = starGo.AddComponent<StarNode>();
            var planetHolder = new GameObject("planetHolder");
            planetHolder.transform.SetParent(starGo.transform);
            starGo.name = star.name;
            return star;
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



