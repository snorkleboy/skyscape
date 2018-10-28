using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using util;

namespace GalaxyCreators
{
    public class galaxyCreator : MonoBehaviour
    {

        [Header("Galaxy Settings")]
        [Space(10)]
        [SerializeField] public List<CreatorWare> creatorStack = new List<CreatorWare>();
        [SerializeField] public Dictionary<int, List<StarNode>> starNodes { get; set; }
        public GameObject holder;
        private bool created = false;
        public void create()
        {
            if (created)
            {
                destroy();
                created = false;
            }
            starNodes = new Dictionary<int, List<StarNode>>();
            foreach (CreatorWare creator in creatorStack)
            {
                creator.actOn(starNodes);
            }

            created = true;
        }

        public void destroy()
        {
            foreach (var keyVal in starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.renderHelper.destroy();
                }
            }
            Util.destroyRecursive(holder.transform);
            starNodes = new Dictionary<int, List<StarNode>>();
        }


    }

}
