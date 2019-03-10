using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]

    public class LinkedAppearer: BaseAppearable{
        public LinkedAppearer(IAppearer appearer,AppearableContainerState linkedState){
            this.thisAppearer = appearer;
            this.linkedState = linkedState;
        } 
        public override AppearableState state{get{return thisAppearer.state;}}
        [SerializeField]private IAppearer thisAppearer;
        public AppearableContainerState linkedState;
        protected override bool _appearImplimentation(int scene){
            sceneI = scene;
            state.isActive = thisAppearer.appear(scene);
            
            foreach (var appearable in linkedState.appearables)
            {
                appearable.appearer.appear(scene);
            }
            return state.isActive;
        }
        public override void destroy(){
            thisAppearer.destroy();
            foreach (var item in linkedState.appearables)
            {
                item.appearer.destroy();
            }
        }
    }
}