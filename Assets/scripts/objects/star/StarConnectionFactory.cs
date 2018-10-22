using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Galaxy
{
    public class StarConnectionFactory : MonoBehaviour
    {
        public GameObject prefab;
        public StarConnection makeConnection(StarNode a, StarNode b)
        {
            var gameObject = Instantiate(prefab, a.representation.transform);
            gameObject.name = "connection";
            var conn = new StarConnection(Random.Range(.01f, .99f), new StarNode[] { a, b }, gameObject); ;
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
