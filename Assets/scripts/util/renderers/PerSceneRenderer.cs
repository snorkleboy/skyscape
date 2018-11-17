using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public abstract class PerSceneRenderer<scriptType> : RenderHelper<scriptType>
    {
        [SerializeField] private GameObject[] _sceneToPrefab;


        private int activeScene = -1;
        public PerSceneRenderer(GameObject[] sceneToPrefab) : base()
        {
            _sceneToPrefab = sceneToPrefab;
        }
        public PerSceneRenderer(GameObject[] sceneToPrefab, Transform parent):base()
        {
            _sceneToPrefab = sceneToPrefab;
            this.parent = parent;
        }
        public override bool render(int scene)
        {
            var prefab = _sceneToPrefab[scene];

            activeGO = GameObject.Instantiate(prefab, parent);

            this.activeScene = scene;
            active = true;
 
            return active;
        }

    }
}
