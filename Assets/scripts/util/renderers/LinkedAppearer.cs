using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Objects.Galaxy
{
    [System.Serializable]
    public partial class LinkedAppearer: BaseAppearable, ISerializationCallbackReceiver{
        public LinkedAppearer(IAppearer appearer){
            this.thisAppearer = appearer;
        }
        public LinkedAppearer(IAppearer appearer,IAppearable appearable):this(appearer){
            this.appearables.Add(appearable);
        }
        public LinkedAppearer(IAppearer appearer,IAppearable[] appearables):this(appearer){
            this.appearables.AddRange(appearables);
        }     
    }

    public partial class LinkedAppearer{
        private IAppearer thisAppearer;
        
        public override Transform appearTransform{get{return thisAppearer.appearTransform;}}
        public List<IAppearable> appearables = new List<IAppearable>();
        public void setAppearables(IEnumerable<IAppearable> renderables){
            this.appearables = new List<IAppearable>();
            addAppearables(renderables);
        }
        public void addAppearables(IEnumerable<IAppearable> renderablesIn)
        {
            this.appearables.AddRange(renderablesIn);
            if(active){
                foreach(var appearable in renderablesIn){
                    appearable.appear(sceneI);
                }
            }
        }
        public void addAppearables(IAppearable renderable)
        {
            this.appearables.Add(renderable);
            if(active){
                renderable.appear(sceneI);
            }
        }
        protected override bool _appearImplimentation(int scene){
            sceneI = scene;
            active = thisAppearer.appear(scene);
            
            foreach (var appearable in appearables)
            {
                appearable.appear(scene);
            }
            return active;
        }

        public override GameObject activeGO{
            get{return thisAppearer.activeGO;}
        }
        public override Vector3 getAppearPosition(int scene){
            return thisAppearer.getAppearPosition(scene);
        }
        public override void setAppearPosition(Vector3 position, int scene){
            thisAppearer.setAppearPosition(position,scene);
        }
        public override void destroy(){
            thisAppearer.destroy();
            foreach (var item in appearables)
            {
                item.appearer.destroy();
            }
        }


        public void OnBeforeSerialize()
        {
            this._appearPosition = thisAppearer.getAppearPosition(SceneManager.GetActiveScene().buildIndex);
            this.activeGO = thisAppearer.activeGO;
            _activeGoes = new List<GameObject>();
            _appearPositions =  new List<SerializableVector3>(); 
            _actives = new List<bool>();
            foreach (var item in this.appearables)
            {
                _activeGoes.Add(item.appearer.activeGO);
                _appearPositions.Add(item.appearer.getAppearPosition(SceneManager.GetActiveScene().buildIndex));
                _actives.Add(item.appearer.isActive);
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