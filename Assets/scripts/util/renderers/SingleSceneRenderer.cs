using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class SingleSceneAppearer : BaseAppearable
    {
        [SerializeField] private GameObject _prefab;
        private int _sceneToAppearOn;
        private sceneAppearInfo _info;
        public SingleSceneAppearer(sceneAppearInfo info, int scene, Transform parent,AppearableState state):base(state)
        {
            _prefab = info.prefab;
            _sceneToAppearOn = scene;
        }

        protected override bool _appearImplimentation(int scene)
        {
            destroy();
            if (scene == _sceneToAppearOn){
                //todo improve
                if(_info.positionOverride != null && _info.positionOverride !=  Vector3.negativeInfinity){
                    state.position = _info.positionOverride;
                }else{
                    state.position = state.position;
                }
                if(state.appearTransform){
                    GameObject.Instantiate(_prefab, state.appearTransform);
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_prefab,_sceneToAppearOn);
                    GameObject.Instantiate(_prefab);
                }
                state.isActive = true;

                return state.isActive;
            }else{
                return state.isActive;
            }
        }
        public override void destroy(){
            base.destroy();
        }

    }
}
