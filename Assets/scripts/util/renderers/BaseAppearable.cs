using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
namespace Objects
{

    [System.Serializable]
    public abstract class BaseAppearable: IAppearer
    {
        protected System.Action<int> preAppear = null;
        protected int sceneI = -1;
        public void withPreAppearHook(System.Action<int> pre){
            preAppear = pre;
        }
        protected System.Action<int> postAppear = null;
        public void withPostAppearHook(System.Action<int> post){
            postAppear = post;
        }

        [SerializeField]protected Vector3 _appearPosition = Vector3.negativeInfinity;
        public virtual Vector3 getAppearPosition(int scene){
            return _appearPosition;
        }
        public virtual void setAppearPosition(Vector3 position, int scene){
            _appearPosition = position;
        }
        [SerializeField]protected GameObject _activeGO;

        [SerializeField]public virtual Transform appearTransform { get; set; }
        [SerializeField]protected bool active = false;
        [SerializeField]public virtual GameObject activeGO
        {
            get{return _activeGO;}
            set{_activeGO = value;}
        }

        public bool isActive{get{return active;}}
        public bool appear(int scene){
            if(preAppear != null){
                preAppear(scene);
            }
            var appeared = _appearImplimentation(scene);
            if(appeared && postAppear !=null){
                postAppear(scene);
            }
            sceneI= scene;
            return appeared;
        }
        protected abstract bool _appearImplimentation(int scene);

        public virtual void destroy()
        {
            if (active)
            {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(activeGO);
#else
            GameObject.Destroy(activeGO);
#endif
                activeGO = null;
                active = false;
            }

        }

    }
    
}
