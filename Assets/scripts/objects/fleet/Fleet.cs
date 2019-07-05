using System.Collections;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects
{
    public static class FleetExt
    {
        public static bool isUsers(this Fleet fleet)
        {
           return fleet.state.factionOwnedState.belongsTo.value.isUsers;
        }
        public static string FleetName(this Fleet fleet)
        {
            return fleet.state.namedState.name;
        }

    }

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
            Debug.Log("FLEET MOUSE ENTER" + ship.state.namedState.name);
            GameManager.instance.UIManager.setHoverObject(this);
            if (this.isUsers())
            {
                var switchers = GetComponentsInChildren<shaderSwitcher>();
                foreach (var switcher in switchers)
                {
                    switcher.toggle();
                }
            }

        }
        public void OnMouseExitShip(Ship ship)
        {
            Debug.Log("FLEET MOUSE Leavee" + ship.state.namedState.name);
            GameManager.instance.UIManager.setHoverObject(null);
            if (this.isUsers())
            {
                var switchers = GetComponentsInChildren<shaderSwitcher>();
                foreach (var switcher in switchers)
                {
                    switcher.toggle();
                }
            }
        }
        public void OnMouseDownShip(Ship ship)
        {
            Debug.Log("FLEET MOUSE Click" + ship.state.namedState.name + " " + this.state.namedState.name);
            if (this.isUsers())
            {
                GameManager.instance.UIManager.setSelectedFleet(this);
            }
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

