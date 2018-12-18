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
        protected Vector3 _appearPosition = Vector3.negativeInfinity;
        public virtual Vector3 getAppearPosition(int scene){
            return _appearPosition;
        }
        public virtual void setAppearPosition(Vector3 position, int scene){
            _appearPosition = position;
        }
        public virtual Transform attachementPoint { get; set; }
        protected bool active = false;
        public virtual GameObject activeGO{get;set;}

        public bool isActive{get{return active;}}
        public abstract bool appear(int scene);

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
