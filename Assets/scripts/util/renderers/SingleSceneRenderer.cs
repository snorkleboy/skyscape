using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public abstract class SingleSceneRenderer<scriptType> : RenderHelper<scriptType>
    {
        [SerializeField] private GameObject _prefab;
        private int _scene;
        public SingleSceneRenderer(GameObject prefab, int scene) : base()
        {
            _prefab = prefab;
            _scene = scene;
        }
        public override bool render(int scene)
        {
            if (scene == _scene){
                if(parent){
                    activeGO = GameObject.Instantiate(_prefab, parent);
                }else{
                    activeGO = GameObject.Instantiate(_prefab);
                }
                active = true;
                return true;
            }else{
                destroy();
                return false;
            }
        }
    }
}
