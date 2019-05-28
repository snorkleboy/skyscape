using System.Collections;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects
{
    [System.Serializable]
    public partial class Fleet:GalaxyGameObject<FleetState>,ISaveable<FleetState>,IViewable,IControllable{

        public void init(FleetState state, LinkedAppearer appearer,FleetMover mover){
            this.state = state;
            _debugfleetState = state;
            this._appearer = appearer;
            this._mover = mover;
            this.fleetController = gameObject.AddComponent<FleetController>().init(mover);
        }
        public FleetState _debugfleetState;

        public InputController controller{get{return fleetController;}}
        private FleetController fleetController;


        public override IAppearer appearer{get{return _appearer;}} 
        private LinkedAppearer _appearer;

        public FleetMover mover{get{return _mover;}}
        private FleetMover _mover;


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

    public class FleetModel{
        public FleetModel(){}
        public FleetModel(Fleet fleet){

            // position = fleet.fleetPosition;
            // var ships = fleet.GetShips();
            // var models = new List<ShipModel>();
            // foreach (var ship in ships)
            // {
            //     models.Add(new ShipModel(ship));
            // }
            // shipModels = models.ToArray();
            // id=fleet.id;
            // factionId = fleet.owningFaction.id;
            // name = fleet.name;
            // // stateAction = fleet.stateAction.model;
        }
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
        public string name;
        public long factionId;
        public long id;
        // public ShipModel[] shipModels;

    }
}

