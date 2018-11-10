﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class StarConnectionFactory : MonoBehaviour
    {
        public GameObject[] _sceneToPrefab;

        public void Start(){
            
            _sceneToPrefab = new GameObject[4];
            _sceneToPrefab[2] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("connection");
            _sceneToPrefab[0] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("justLine");
            _sceneToPrefab[3] = AssetSingleton.getBundle(AssetSingleton.bundleNames.prefabs).LoadAsset<GameObject>("starViewConnection");
            Debug.Log("STARVIEW CONNECTION PREFAB:" + _sceneToPrefab[3]);
        }
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