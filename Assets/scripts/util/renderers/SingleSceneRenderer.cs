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

        private bool savedPosition = false;
        private Vector3 positionSave = new Vector3(-999,999,-999);

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
                //todo improve

                if(state.appearTransform){
                    state.activeTransform = GameObject.Instantiate(_info.prefab, state.appearTransform).transform;
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_info.prefab,_sceneToAppearOn);
                    state.activeTransform = GameObject.Instantiate(_info.prefab).transform;
                }
                 if(_info.shouldOveride){
                     Debug.Log("positionOverride  " + this + " " + _info.positionOverride.x + " " + _info.positionOverride.y );
                     positionSave = state.position;
                     state.position = _info.positionOverride;
                     savedPosition = true;
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
            if(savedPosition){
                state.position = positionSave;
                positionSave = new Vector3(-999,999,-999);
                savedPosition = false;
            }
        }

    }
}
