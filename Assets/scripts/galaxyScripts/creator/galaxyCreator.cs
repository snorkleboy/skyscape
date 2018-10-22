using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public class galaxyCreator : MonoBehaviour
    {

        [Header("Galaxy Settings")]
        [Space(10)]
        //[Range(1, 50)]
        //[SerializeField] private int featherSize = 5;
        //[Range(.01f, 1)]
        //[SerializeField] private double emptySystemRate = .4;
        //[Range(1, 20)]
        //[SerializeField] private int planaterySystemAverageSize = 4;
        //private int interFeatherConnectedness;
        //private int interBranchConnectedness;
        //private int outerConnectedNess;
        //private int innerConnectedNess;

        [SerializeField] public List<CreatorWare> creatorStack = new List<CreatorWare>();
        [SerializeField] private Dictionary<int, StarNode[]> starNodes = new Dictionary<int, StarNode[]>();
        public GameObject holder;
        private bool created = false;
        public void create()
        {
            if (created)
            {
                destroy();
                created = false;

            }
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
                    star.representation.destroy();
                }
            }
            destroyRecursive(holder.transform);
            starNodes = new Dictionary<int, StarNode[]>();
        }
        public void destroyRecursive(Transform parent)
        {
            if(parent == null)
            {
                return;
            }
            foreach (Transform trans in parent) {
                destroyRecursive(trans);
#if UNITY_EDITOR
                GameObject.DestroyImmediate(trans.gameObject);
#else
                            GameObject.Destroy(trans.gameObject);
#endif
            }
        }

    }

}
