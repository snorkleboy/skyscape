using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public class CreatorWare : MonoBehaviour
    {
        public virtual Dictionary<int, StarNode[]> actOn(Dictionary<int, StarNode[]> starNodes)
        {
            return starNodes;
        }
    }

    public class StarMaker : CreatorWare
    {
        [SerializeField]
        public GameObject baseStarFab;
        [SerializeField]
        public GameObject holder;

        
        protected StarRepresentation newStarRepresention(Transform holder)
        {
            var star = new StarRepresentation(baseStarFab, holder);
            return star;
        }
    }

}

