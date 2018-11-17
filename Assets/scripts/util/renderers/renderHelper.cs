using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
namespace Objects
{
    [System.Serializable]
    public abstract class RenderHelper<ScriptSource>: IRenderer
    {
        public ScriptSource scriptSingelton { get; set; }
        protected bool active = false;
        public abstract void applyScript(GameObject activeGO, ScriptSource scriptSingelton);
        public abstract bool render(int scene);
        public RenderHelper()
        {
            uid = Guid.NewGuid();
        }
        public Transform parent { get; set; }
        protected GameObject _activeGO;
        [SerializeField]
        protected GameObject activeGO
        {
            get { return _activeGO; }
            set { _activeGO = value; if (value != null) { applyScript(_activeGO, scriptSingelton); } }
        }
        public Transform transform
        {
            get
            {
                if (activeGO) { return activeGO.transform; }
                else {
                    return null;
                 }
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
            GameObject.Destroy(activeGO);
#endif
                activeGO = null;
                active = false;
            }

        }

    }
    
}
