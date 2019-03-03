﻿using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{

    public struct sceneAppearInfo{
        public sceneAppearInfo(GameObject prefab){
            this.prefab = prefab;
            this.positionOverride = Vector3.negativeInfinity;
            shouldOveride = false;
        }
        public sceneAppearInfo(GameObject prefab,Vector3 pos){
            this.prefab = prefab;
            this.positionOverride = pos;
            shouldOveride = true;
        }
        public GameObject prefab;
        public bool shouldOveride;
        public Vector3 positionOverride;
    }
    [System.Serializable]
    public class MultiSceneAppearer : BaseAppearable
    {
        public override AppearableState state{get;set;}

        public MultiSceneAppearer(sceneAppearInfo[] appearInfo,AppearableState state)
        {
            this.state = state;
            sceneRenderers = new SingleSceneAppearer[appearInfo.Length];
            for(var i =0; i<appearInfo.Length;i++){
                sceneRenderers[i] = new SingleSceneAppearer(appearInfo[i],i,state);
            }
        }
        private int activeScene = -1;
        [SerializeField]private SingleSceneAppearer[] sceneRenderers;

        protected override bool _appearImplimentation(int scene)
        {
            this.activeScene = scene;
            if(state.isActive){
                destroy();
            }
            var ren = sceneRenderers[scene];
            if(ren != null){
                state.isActive = ren.appear(scene);
            }
            return state.isActive;
        }

    }
}
