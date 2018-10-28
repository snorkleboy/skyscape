using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Galaxy
{
    public class StarConnectionFactory : MonoBehaviour
    {
        public GameObject[] _sceneToPrefab;
        public StarConnection makeConnection(StarNode a, StarNode b)
        {
            gameObject.name = "connection";
            var starNodes = new StarNode[] { a, b };
            var renderer = new StarConnectionRenderHelper(_sceneToPrefab, starNodes);
            renderer.parent = a.transform;
            var conn = new StarConnection(Random.Range(.01f, .99f), starNodes, renderer);
            conn.render(0);
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
