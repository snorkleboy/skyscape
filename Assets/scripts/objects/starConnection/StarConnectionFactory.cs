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
            a.enterable.addConnection(conn);
            b.enterable.addConnection(conn);
            return conn;
        }
        public StarConnection makeConnection(StarNode nodeInstance,Reference<StarNode> referencedStarNode){
            StarConnection conn = null;
            if(referencedStarNode.checkExists()){
                conn = referencedStarNode.value.enterable.getConnection(nodeInstance.state.id);
            }
            if (conn == null){
                conn = _makeConnection(nodeInstance,referencedStarNode);
            }
            nodeInstance.enterable.addConnection(conn);
            return conn;
        }
        private StarConnection _makeConnection(StarNode nodeInstance,Reference<StarNode> referencedStarNode){
            var state = new StarConnectionState(){
                appearableState = new State.AppearableState(
                    appearTransform:nodeInstance.appearer.state.appearTransform,
                    position: new Vector3(-99999,-99999,-99999),
                    star:null
                ),
                strength = Random.Range(.01f, .99f),
                nodes = new Reference<StarNode>[] {new Reference<StarNode>(nodeInstance), referencedStarNode}
            };
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }
            var conn = nodeInstance.gameObject.AddComponent<StarConnection>();
            var renderer = new StarConnectionAppearer(conn,infos, state);
            conn.Init(state, renderer);
            return conn;
        }
    }
}
