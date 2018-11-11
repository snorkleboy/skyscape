using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Util;

namespace GalaxyCreators
{

    public class galaxyCreator<T> : MonoBehaviour
    {

        [Header("Galaxy Settings")]
        [Space(10)]
        [SerializeField] public List<ICreator<T>> creatorStack = new List<ICreator<T>>();
        [SerializeField] public Dictionary<int, List<T>> starNodes { get; set; }
        public GameObject holder;
        protected bool created = false;
        public virtual void create()
        {
            Debug.Log("CREATE GALAXY");
            if (created)
            {
                destroy();
                created = false;
            }
            starNodes = new Dictionary<int, List<T>>();
            foreach (ICreator<T> creator in creatorStack)
            {
                creator.actOn(starNodes);
            }

            created = true;
        }
        public void destroy()
        {
            Util.Util.destroyRecursive(holder.transform);
            starNodes = new Dictionary<int, List<T>>();
        }
    }

}
