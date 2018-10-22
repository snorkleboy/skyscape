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
            private List<StarConnection> connections { get; set; }
            public void addConnection(StarConnection connection)
            {
                connections.Add(connection);
            }
            public StarNode(IRepresentable representation)
            {
                this.representation = representation;
                connections = new List<StarConnection>();
            }
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

        [System.Serializable]
        public class StarConnection
        {
            public StarConnection(double _strength, StarNode[] _nodes, GameObject _prefab)
            {
                strength = _strength;
                nodes = _nodes;
                representation = _prefab;
                var line =(DrawLineBetweenPoints) representation.GetComponent<DrawLineBetweenPoints>();
                line.setTarget(_nodes[0].representation.transform.gameObject,0);
                line.setTarget(_nodes[1].representation.transform.gameObject,1);
                line.draw();
            }

            public double strength;
            public StarNode[] nodes;
            public GameObject representation;
        }
    }
}
