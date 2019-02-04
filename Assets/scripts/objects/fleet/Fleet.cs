using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects
{
    public class FleetModel{
        public FleetModel(){}
        public FleetModel(Fleet fleet){

            position = fleet.fleetPosition;
            var ships = fleet.GetShips();
            var models = new List<ShipModel>();
            foreach (var ship in ships)
            {
                models.Add(new ShipModel(ship));
            }
            shipModels = models.ToArray();
            id=fleet.id;
            factionId = fleet.owningFaction.id;
            name = fleet.name;
            stateAction = fleet.stateAction.model;
        }
        public SerializableVector3 position;
        public string name;
        public long factionId;
        public long id;
        public ShipModel[] shipModels;
        public StateActionModel stateAction;

    }
    //setup
    public partial class Fleet:MoveAbleGameObject,ISaveAble<FleetModel>,IViewable,ISelectable,IIded{
        public FleetModel model{get{return new FleetModel(this);}}
        public long id;
        public Faction owningFaction;
        public long getId(){return id;}
        public void Init(string name, Sprite icon, LinkedAppearer renderHelper,Vector3 position,Faction faction){
            this.name = name;
            this.icon = icon;
            this.fleetPosition = position;
            this._appearer = renderHelper;
            var fleetMover = gameObject.AddComponent<FleetMover>();
            ships = new ShipManager(fleetMover);
            owningFaction = faction;
        }
        public List<inputAction> controls;
        public void Awake(){
            controls = new List<inputAction>()
                {
                    new inputAction(checkClick,moveTo),
                };
        }
        public bool checkClick(){
            var down = Input.GetMouseButtonDown(1);
            return down;
        }
        public void moveTo(){
            Debug.Log("set position " + this);
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                var newvec = hit.point;
                newvec.y = 0;
                setStateAction(FleetStateActions.moveFleet(this,newvec));
            }else{
                Debug.Log("no hit");
            }
        }
    }
    public partial class Fleet
    {
        public override IMover mover{get{return ships.mover;}}
        public Vector3 fleetPosition;
        public ShipManager ships;
        public Vector3 getPosition(){
            return ships.getPostion();
        }
        public Fleet addShips(Ship ship){
            this.ships.addShips(ship);
            _appearer.addAppearables(ship);
            return this;
        }
        public Fleet addShips(List<Ship> ships){
            this.ships.addShips(ships) ;
            _appearer.addAppearables(ships.ToArray());
            return this;
        }
        public IEnumerable<Ship> GetShips(){return ships.ships;}

    }
    // rendering
    public partial class Fleet{
        public override IAppearer appearer{get{return _appearer;}} 
        private LinkedAppearer _appearer;
        public override void appear(int scene){
            var count = 0;
            foreach (var appearable in _appearer.appearables){
                var appearPos = fleetPosition + new Vector3(1 + 3*count++,0,0);
                appearable.appearer.setAppearPosition(appearPos,3);
            }
            if(appearer.appear(scene)){
                appearer.activeGO.transform.position = fleetPosition;
                this.ships.mover.fleetTransform = appearer.activeGO.transform;
            }

        }
    }
    //ui
    public partial class Fleet {
        public InputController getInputController(GameObject parent){
            var controller = parent.AddComponent<InputController>();
            controller.Init(controls ,this.gameObject);
            return controller;
        }
        public override iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = name;
            info.icon = icon;
            return info;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            return new GameObject();
        }

        public static class FleetStateActions{
            public static StateAction moveFleet(Fleet fleet,Vector3 target){
                return new MoveFleet().Init(fleet, target);
            }
            public static StateAction moveFleet(Fleet fleet,MoveFleetModel model){
                return new MoveFleet().Init(fleet, model.target);
            }
        }
    }

}