using System.Collections;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects
{


    [System.Serializable]
    public partial class Fleet:GalaxyGameObject<FleetState>,IViewable,IControllable
    {

        public void init(FleetState state, LinkedAppearer appearer){
            this.state = state;
            _debugfleetState = state;
            this._appearer = appearer;
            this.fleetController = gameObject.AddComponent<FleetController>().init(this);
        }
        public FleetState _debugfleetState;

        public InputController controller{get{return fleetController;}}
        private FleetController fleetController;


        public override IAppearer appearer{get{return _appearer;}} 
        private LinkedAppearer _appearer;

        public override IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = state.namedState.name;
            // info.icon = icon;
            return info;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            return new GameObject();
        }
    }

}

