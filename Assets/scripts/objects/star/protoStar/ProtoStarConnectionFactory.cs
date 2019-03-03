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
            var state = new ProtoStarConnectionState(){nodes = new ProtoStar[] { a, b }};
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }

            var renderer = new ProtoStarConnectionRenderer(infos, state);
            var conn = new ProtoStarConnection();
            conn.Init( state, renderer);
            a.state.addConnection(conn);
            b.state.addConnection(conn);
            conn.appearer.appear(0);
            return conn;
        }
    }
}
