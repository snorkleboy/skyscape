using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class SingleSceneAppearer : BaseAppearable
    {
        [SerializeField] private GameObject _prefab;
        public override void setAppearPosition(Vector3 position, int scene){
            _appearPosition = position;
            _scene = scene;
        }
        private int _scene;
        public SingleSceneAppearer(sceneAppearInfo info, int scene, Transform parent)
        {
            _prefab = info.prefab;
            _scene = scene;
            this.attachementPoint = parent;
            this._appearPosition = info.appearPosition;
        }

        private int activeScene = -1;
        protected override bool _appearImplimentation(int scene)
        {
            activeScene = scene;
            destroy();
            if (scene == _scene){
                if(attachementPoint){
                    activeGO = GameObject.Instantiate(_prefab, attachementPoint);
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_prefab,_scene);
                    activeGO = GameObject.Instantiate(_prefab);
                }
                active = true;
                activeGO.transform.position = _appearPosition;
                return active;
            }else{
                return active;
            }
        }
        public override void destroy(){
            if(activeScene != _scene){
                base.destroy();
            }
        }

    }
}
