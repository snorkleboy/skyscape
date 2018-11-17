using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class StarConnectionFactory : MonoBehaviour
    {
        public GameObject[] _sceneToPrefab;

        public void Awake(){
            
            _sceneToPrefab = new GameObject[4];
            _sceneToPrefab[2] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("connection");
            _sceneToPrefab[0] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("justLine");
            _sceneToPrefab[3] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("starViewConnection");
        }
        public StarConnection makeConnection(StarNode a, StarNode b)
        {
            var starNodes = new StarNode[] { a, b };
            var renderer = new StarConnectionRenderHelper(_sceneToPrefab, starNodes);
            // renderer.parent = a.transform.Find("representation");
            var conn = a.gameObject.AddComponent<StarConnection>();
            conn.Init(Random.Range(.01f, .99f), starNodes, renderer);
            // conn.render(0);
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
