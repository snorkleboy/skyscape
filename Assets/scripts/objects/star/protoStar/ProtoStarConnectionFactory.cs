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
            var conn = new ProtoStarConnection();
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i], Vector3.zero);
            }
            infos[0].appearPosition = a.appearer.getAppearPosition(0);
            var renderer = new ProtoStarConnectionRenderer(infos, starNodes);
            conn.Init(Random.Range(.01f, .99f), starNodes, renderer);
            renderer.appearTransform = a.transform;


            conn.appear(0);
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
