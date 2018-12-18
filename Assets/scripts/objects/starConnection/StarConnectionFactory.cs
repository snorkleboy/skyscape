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
            _sceneToPrefab[3] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("starViewConnection");
        }
        public StarConnection makeConnection(StarNode a, StarNode b)
        {
            var starNodes = new StarNode[] { a, b };
            // renderer.parent = a.transform.Find("representation");
            var conn = a.gameObject.AddComponent<StarConnection>();

            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }
            infos[2].appearPosition = a.appearer.getAppearPosition(2);
            infos[3].appearPosition = Vector3.zero;
            
            var renderer = new StarConnectionAppearer(infos, conn,starNodes);
            conn.Init(Random.Range(.01f, .99f), starNodes, renderer);
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
    }
}
