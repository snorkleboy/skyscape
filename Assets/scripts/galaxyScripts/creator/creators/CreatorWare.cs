using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public class CreatorWare : MonoBehaviour
    {
        public virtual Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> starNodes)
        {
            return starNodes;
        }
    }

    public class StarMaker : CreatorWare
    {
        [SerializeField]
        public StarFactory starFactory;
        [SerializeField]
        public GameObject holder;
    }

}

