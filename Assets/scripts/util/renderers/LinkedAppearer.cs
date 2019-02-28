using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    [System.Serializable]


    public class LinkedAppearer: BaseAppearable, ISerializationCallbackReceiver{
        public LinkedAppearer(IAppearer appearer,AppearableContainerState linkedState,AppearableState state):base(state){
            this.thisAppearer = appearer;
            this.linkedState = linkedState;
        } 
        private IAppearer thisAppearer;
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


        public void OnBeforeSerialize()
        {
            _activeGoes = new List<GameObject>();
            _appearPositions =  new List<SerializableVector3>(); 
            _actives = new List<bool>();
            foreach (var item in linkedState.appearables)
            {
                _activeGoes.Add(state.appearTransform.gameObject);
                _appearPositions.Add(item.appearer.state.position);
                _actives.Add(item.appearer.state.isActive);
            } 
        }
        public void OnAfterDeserialize()
        {
            _activeGoes = null;
            _appearPositions = null;     
            _actives= null;
        }

        [SerializeField]private List<bool> _actives;
        [SerializeField]private List<GameObject> _activeGoes;
        [SerializeField]private List<SerializableVector3> _appearPositions;
    }
}