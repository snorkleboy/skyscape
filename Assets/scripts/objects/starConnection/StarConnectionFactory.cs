using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
using System.Linq;
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
            var conn = _makeConnection(a,new Reference<StarNode>(b));
            // renderer.parent = a.transform.Find("representation");
            a.addConnection(conn);
            b.addConnection(conn);
            return conn;
        }
        public StarConnection makeConnection(StarNode nodeInstance,Reference<StarNode> referencedStarNode){
            StarConnection conn = null;
            bool existed = false;
            if(referencedStarNode.checkExists()){
                existed = true;
                conn = referencedStarNode.value.connectable.getConnection(nodeInstance.id);
            }
            if (conn == null){
                conn = _makeConnection(nodeInstance,referencedStarNode);
            }
            nodeInstance.addConnection(conn);
            Debug.Log("connection make " + existed + " refId:" +referencedStarNode.getId() + "instId:"+nodeInstance.id + " conn==null=" + (conn==null));
            Debug.Log(("connection make " + conn.nodes[0].getId() + " " + conn.nodes[1].getId()));
            return conn;
        }
        private StarConnection _makeConnection(StarNode nodeInstance,Reference<StarNode> referencedStarNode){
            var starNodes = new Reference<StarNode>[] {new Reference<StarNode>(nodeInstance), referencedStarNode};
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }
            infos[2].appearPosition = nodeInstance.appearer.getAppearPosition(2);
            infos[3].appearPosition = Vector3.zero;
            var conn = nodeInstance.gameObject.AddComponent<StarConnection>();
            var renderer = new StarConnectionAppearer(infos, conn,starNodes);
            conn.Init(Random.Range(.01f, .99f), starNodes, renderer);
            Debug.Log("connection make ___INIT__" + " refId:" +referencedStarNode.getId() + "instId:"+nodeInstance.id + " conn==null =" + (conn==null));
            return conn;
        }
    }
}
