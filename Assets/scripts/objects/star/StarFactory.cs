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

        public virtual StarNode newStar(Transform holder)
        {
            return createStar(holder);
        }
        public StarNode createStar(Transform holder)
        {
            var rep = new StarRenderer(_sceneToPrefab, holder);
            var star = new StarNode(rep,starIconSprites[0]);
            star.render(0);
            return star;
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



