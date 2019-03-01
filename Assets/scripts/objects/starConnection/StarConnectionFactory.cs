﻿using System.Collections;
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
            a.enterable.addConnection(conn);
            b.enterable.addConnection(conn);
            return conn;
        }
        public StarConnection makeConnection(StarNode nodeInstance,Reference<StarNode> referencedStarNode){
            StarConnection conn = null;
            bool existed = false;
            if(referencedStarNode.checkExists()){
                existed = true;
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
                appearableState = nodeInstance.appearer.state,
                strength = Random.Range(.01f, .99f),
                nodes = new Reference<StarNode>[] {new Reference<StarNode>(nodeInstance), referencedStarNode}
            };
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for(var i=0;i<_sceneToPrefab.Length;i++){
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);
            }
            var conn = nodeInstance.gameObject.AddComponent<StarConnection>();
            var renderer = new StarConnectionAppearer(conn,infos, state,nodeInstance.appearer.state);
            conn.Init(state, renderer);
            return conn;
        }
    }
}
