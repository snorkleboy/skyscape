using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public abstract class PerSceneRenderer<scriptType> : RenderHelper<scriptType>
    {
        [SerializeField] private GameObject[] _sceneToPrefab;
        public override Transform parent { get; set; }


        private int activeScene = -1;
        public PerSceneRenderer(GameObject[] sceneToPrefab, Transform parent, scriptType script):base(script)
        {
            _sceneToPrefab = sceneToPrefab;
            this.parent = parent;
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
 
            return active;
        }

    }
}
