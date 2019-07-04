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

        public void OnMouseEnterShip(Ship ship)
        {
            Debug.Log("MOUSE ENTER" + ship.state.namedState.name);
            var switchers = GetComponentsInChildren<shaderSwitcher>();
            foreach(var switcher in switchers)
            {
                switcher.toggle();
            }
        }
        public void OnMouseExitShip(Ship ship)
        {
            Debug.Log("go MOUSE Leave" + ship.state.namedState.name);
            var switchers = GetComponentsInChildren<shaderSwitcher>();
            foreach (var switcher in switchers)
            {
                switcher.toggle();
            }
        }
        public void OnMouseDownShip(Ship ship)
        {
            Debug.Log("go MOUSE Click" + ship.state.namedState.name);
        }
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

