using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects
{
    [System.Serializable]
    public abstract class RenderHelper: IRenderer
    {
        protected bool active = false;

        public abstract bool render(int scene);
        public RenderHelper()
        {
            uid = Guid.NewGuid();
        }
        public Transform parent { get; set; }
        [SerializeField] protected GameObject activeGO;
        public Transform transform
        {
            get
            {
                if (activeGO) { return activeGO.transform; }
                else { return null; }
            }
        }
        public Guid uid { get; }

        public virtual void enable()
        {
            activeGO.SetActive(true);
        }
        public virtual void disable()
        {
            activeGO.SetActive(false);
        }
        public virtual void destroy()
        {
            if (active)
            {
#if UNITY_EDITOR
                GameObject.DestroyImmediate(activeGO);
#else
            GameObject.Destroy(active);
#endif
                activeGO = null;
                active = false;
            }

        }

    }
    
}
