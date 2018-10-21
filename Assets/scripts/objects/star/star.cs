using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    namespace Galaxy
    {

        [System.Serializable]
        public class StarNode
        {
            public IRepresentable representation { get; set; }
            public Transform transform { get { return representation.transform; } }
            private StarConnection[] connections { get; set; }

            public StarNode(IRepresentable representation)
            {
                this.representation = representation;
            }
        }

        [System.Serializable]
        public class StarConnection
        {
            double strength;
            StarNode[] nodes;
        }
        [System.Serializable]
        public class StarRepresentation : IRepresentable
        {
            public GameObject representation { get; set; }
            public Transform transform { get; set; }
            [SerializeField] private Color color;
            [SerializeField] private int scale;
            [SerializeField] private int numPlanets;
            [SerializeField] private int brightness;

            public StarRepresentation(GameObject prefab, Transform parent)
            {
                representation = GameObject.Instantiate(prefab, parent);
                transform = representation.transform;
            }

            public void enter()
            {
                representation.SetActive( true);
            }
            public void exit()
            {
                representation.SetActive( false);
            }
            public void destroy()
            {
                #if UNITY_EDITOR
                    GameObject.DestroyImmediate(representation);
                #else
                    GameObject.Destroy(representation);
                #endif
            }
        }
    }
}
