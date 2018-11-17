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
            }else{
                Debug.Log("loaded StarIcon Sprites  "+starIconSprites.Length  );
            }
        }

        public virtual StarNode newStar(Transform holder)
        {
            return createStar(holder);
        }
        public StarNode createStar(Transform holder, Vector3 position){
            var node = createStar(holder);
            node.transform.position = position;
            return node;
        }

        public StarNode createStar(Transform holder)
        {
            var starGo = new GameObject("starNode");
            starGo.transform.SetParent(holder);
            var star = starGo.AddComponent<StarNode>();
            var planetHolder = new GameObject("planetHolder");
            planetHolder.transform.SetParent(starGo.transform);
            var representation = new GameObject("representation");
            representation.transform.SetParent(starGo.transform);
            var rep = new StarRenderer(_sceneToPrefab, representation.transform);
            star.Init(rep,starIconSprites[0]);
            return star;
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



