using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Objects.Galaxy.State;
namespace Objects
{

    [System.Serializable]
    public abstract partial class BaseAppearable: IAppearer
    {
        public virtual AppearableState state{get;private set;}
        public BaseAppearable(AppearableState state){
            this.state = state;
        }
        public virtual void setAppearTransform(Transform transform){
            state.appearTransform = transform;
        }
        protected int sceneI = -1;
        public virtual void destroy()
        {
            if (state.isActive)
            {
                var obj = state.appearTransform.gameObject;
#if UNITY_EDITOR
            GameObject.DestroyImmediate(obj);
#else
            GameObject.Destroy(obj);
#endif
                state.isActive = false;
            }

        }

    }
    //appear
    public partial class BaseAppearable{
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
