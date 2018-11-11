using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class ProtoStarConnectionFactory : MonoBehaviour
    {
        public GameObject[] _sceneToPrefab;

        public ProtoStarConnectionFactory()
        {
            _sceneToPrefab = new GameObject[1];
        }
        public ProtoStarConnection makeConnection(ProtoStar a, ProtoStar b)
        {
            var starNodes = new ProtoStar[] { a, b };
            var renderer = new ProtoStarConnectionRenderer(_sceneToPrefab, starNodes);
            renderer.parent = a.transform;
            var conn = new ProtoStarConnection(Random.Range(.01f, .99f), starNodes, renderer);
            conn.render(0);
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
