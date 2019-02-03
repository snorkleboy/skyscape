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
            _sceneToAppearOn = scene;
        }
        private int _sceneToAppearOn;
        public SingleSceneAppearer(sceneAppearInfo info, int scene, Transform parent)
        {
            _prefab = info.prefab;
            _sceneToAppearOn = scene;
            this.appearTransform = parent;
            this._appearPosition = info.appearPosition;
        }

        protected override bool _appearImplimentation(int scene)
        {
            destroy();
            if (scene == _sceneToAppearOn){
                if(appearTransform){
                    activeGO = GameObject.Instantiate(_prefab, appearTransform);
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_prefab,_sceneToAppearOn);
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
            base.destroy();
        }

    }
}
