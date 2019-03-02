using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class SingleSceneAppearer : BaseAppearable
    {
        [SerializeField] protected GameObject _prefab;
        protected int _sceneToAppearOn;
        protected sceneAppearInfo _info;
        public SingleSceneAppearer(sceneAppearInfo info, int scene,AppearableState state):base(state)
        {
            _prefab = info.prefab;
            _sceneToAppearOn = scene;
        }

        protected override bool _appearImplimentation(int scene)
        {
            destroy();
            if (scene == _sceneToAppearOn){
                //todo improve

                if(state.appearTransform){
                    state.activeTransform = GameObject.Instantiate(_prefab, state.appearTransform).transform;
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_prefab,_sceneToAppearOn);
                    state.activeTransform = GameObject.Instantiate(_prefab).transform;
                }
                if(_info.positionOverride != null && _info.positionOverride !=  Vector3.negativeInfinity){
                    state.position = _info.positionOverride;
                }else{
                    state.position = state.position;
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
