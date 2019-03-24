using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class SingleSceneAppearer : BaseAppearable
    {
        //  protected GameObject _prefab;
        protected int _sceneToAppearOn;
        protected sceneAppearInfo _info;
        protected Vector3 savedPosition;
        protected bool positionSaved = false;
        public SingleSceneAppearer(sceneAppearInfo info, int scene,AppearableState state)
        {
            this._info = info;
            _sceneToAppearOn = scene;
            this.state = state;
        }

        protected override bool _appearImplimentation(int scene)
        {
            destroy();
            if (scene == _sceneToAppearOn){
                if(state.appearTransform){
                    state.activeTransform = GameObject.Instantiate(_info.prefab, state.appearTransform).transform;
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_info.prefab,_sceneToAppearOn);
                    state.activeTransform = GameObject.Instantiate(_info.prefab).transform;
                }
                
                if(_info.shouldOveride){
                    savedPosition = state.position;
                    state.position = _info.positionOverride;
                    positionSaved = true;
                }else{
                    state.position = state.position;
                }
                state.rotation = state.rotation;
                state.isActive = true;
            }
            return state.isActive;

        }
        public override void destroy(){
            base.destroy();
            if(positionSaved){
                Debug.Log("UN-OVERRIDING POSITION");
                state.position = savedPosition;
                positionSaved = false;
            }
        }

    }
}
