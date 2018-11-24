﻿using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public abstract class PerSceneRenderer<scriptType> : RenderHelper<scriptType>
    {
        [SerializeField] private GameObject[] _sceneToPrefab;
        public override Transform parent { get; set; }
        public Vector3 position;


        private int activeScene = -1;
        public PerSceneRenderer(GameObject[] sceneToPrefab, Transform parent, scriptType script):base(script)
        {
            _sceneToPrefab = sceneToPrefab;
            this.parent = parent;
            this.position = parent.position;
        }
        public override bool render(int scene)
        {
            if(active){
                destroy();
            }
            var prefab = _sceneToPrefab[scene];
            activeGO = GameObject.Instantiate(prefab, parent);
            this.activeScene = scene;
            active = true;
            if (position != null){
                activeGO.transform.position = position;
            }
 
            return active;
        }

    }
}
