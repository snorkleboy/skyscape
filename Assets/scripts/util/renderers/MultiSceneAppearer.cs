using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    public struct sceneAppearInfo{
        public sceneAppearInfo(GameObject prefab){
            this.prefab = prefab;
            this.appearPosition = Vector3.negativeInfinity;
        }
        public sceneAppearInfo(GameObject prefab,Vector3 pos){
            this.prefab = prefab;
            this.appearPosition = pos;
        }
        public GameObject prefab;
        public Vector3 appearPosition;
    }
    [System.Serializable]
    public class MultiSceneAppearer : BaseAppearable
    {
        public MultiSceneAppearer(sceneAppearInfo[] appearInfo, Transform parent)
        {
            this.appearTransform = parent;
            sceneRenderers = new SingleSceneAppearer[appearInfo.Length];
            for(var i =0; i<appearInfo.Length;i++){
                sceneRenderers[i] = new SingleSceneAppearer(appearInfo[i],i,parent);
            }
        }

        public override void setAppearPosition(Vector3 position, int scene){
            if(sceneRenderers[scene] != null){
                    sceneRenderers[scene].setAppearPosition(position,scene);
            }else{
                util.Log.warnLog(this,"setAppearPosition called on empty prefab", "activeScene",activeScene,"active",active);
            }
        }
        public override Vector3 getAppearPosition(int scene)
        {
            if(sceneRenderers[scene] != null){
                return sceneRenderers[scene].getAppearPosition(scene);
            }else{
                return Vector3.negativeInfinity;
            }
        }
        public override Transform appearTransform { get; set; }
        private int activeScene = -1;
        [SerializeField]private SingleSceneAppearer[] sceneRenderers;

        protected override bool _appearImplimentation(int scene)
        {
            this.activeScene = scene;
            if(active){
                destroy();
            }
            var ren = sceneRenderers[scene];
            if(ren != null){
                active = sceneRenderers[scene].appear(scene);
                activeGO = sceneRenderers[scene].activeGO;
                _appearPosition = sceneRenderers[scene].getAppearPosition(scene);
            }
            return active;
        }
        public override GameObject activeGO{
            get{
                return _activeGO;           
            }
            set{_activeGO = value;}
        }

    }
}
