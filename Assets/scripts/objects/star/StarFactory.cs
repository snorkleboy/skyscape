using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Galaxy
{
    public class StarFactory : MonoBehaviour
    {
        [SerializeField] public GameObject[] _sceneToPrefab;
        [SerializeField] public StarConnectionFactory starConnectionFactory;
        [SerializeField] public PlanetFactory planetfactory;

        public virtual StarNode newStar(Transform holder)
        {
            return createStar(holder);
        }
        public StarNode createStar(Transform holder)
        {
            var rep = new HolderRenderer(_sceneToPrefab, holder);
            var star = new StarNode(rep);
            star.render(0);
            return star;
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



