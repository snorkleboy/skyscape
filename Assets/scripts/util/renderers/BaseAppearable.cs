using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Objects.Galaxy.State;
namespace Objects
{

    [System.Serializable]
    public abstract class BaseAppearable: IAppearer
    {
        public abstract AppearableState state{get;set;}

        protected int sceneI = -1;
        public virtual void destroy()
        {
            if (state.isActive)
            {
#if UNITY_EDITOR
    GameObject.DestroyImmediate(state.activeTransform.gameObject);
#else
    GameObject.Destroy(state.activeTransform.gameObject);
#endif
                state.isActive = false;
            }

        }

        protected System.Action<int> preAppear = null;
        public void withPreAppearHook(System.Action<int> pre){
            preAppear = pre;
        }
        protected System.Action<int> postAppear = null;
        public void withPostAppearHook(System.Action<int> post){
            postAppear = post;
        }
        public bool appear(int scene){
            if(preAppear != null){
                preAppear(scene);
            }
            var appeared = _appearImplimentation(scene);
            if(appeared && postAppear !=null){
                postAppear(scene);
            }
            sceneI = scene;
            return appeared;
        }
        protected abstract bool _appearImplimentation(int scene);
    }
    
}
