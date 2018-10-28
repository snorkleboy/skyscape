using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public abstract class SingleSceneRenderer<scriptType> : RenderHelper<scriptType>
    {
        [SerializeField] private GameObject _prefab;

        public SingleSceneRenderer(GameObject prefab) : base()
        {
            _prefab = prefab;
        }
        public override bool render(int scene)
        {
            activeGO = GameObject.Instantiate(_prefab, parent);
            active = true;
            return true;
        }
    }
}
