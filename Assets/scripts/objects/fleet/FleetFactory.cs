using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Conceptuals;
using Objects.Galaxy.State;
namespace Objects
{
    public class FleetFactory : MonoBehaviour
    {
        public ShipFactory shipFactory;
        public Sprite icon;
        public GameObject[] sceneToPrefab = new GameObject[4];

        public void Awake(){
            shipFactory = gameObject.AddComponent<ShipFactory>();
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0];
            if(icon == null){
                Debug.LogWarning("fleet factory did not find icon");
            }
            sceneToPrefab[3] = AssetSingleton.getAsset<GameObject>(AssetSingleton.bundleNames.prefabs, "fleetGO");//.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ui")
            if(sceneToPrefab[3] == null){
                Debug.LogWarning("fleet factory did not find fleet prefab");
            }
        }
        public Fleet makeFleet(FleetState state,Dictionary<long,object> stateTable ){
            var faction = state.factionOwnedState.belongsTo;
            var starAt = state.positionState.starAt;
            GameObject fleetGo;
            var fleet =  makeTransforms(starAt,out fleetGo);
            fleet.state = state;
            GameManager.idMaker.insertObject(fleet,state.id);
            var fleetState = hydrateState(state,fleetGo.transform);
            var fleetRenderer = makeAppearers(fleetState);
            var mover = fleetGo.AddComponent<FleetMover>().init(fleet);
            fleet.init(fleetState,fleetRenderer,mover);
            fleetGo.name = fleet.name;
            
            for(var i=0;i<state.shipsContainer.ships.Count;i++)
            {
                var shipRef = state.shipsContainer.ships[i];
                var stateObject = stateTable[shipRef.id];
                var shipState = (ShipState)stateObject;
                var ship = shipFactory.makeShip(shipState,fleet);
                state.shipsContainer.appearables.Add(ship);
            }
            if(state.actionState.stateAction != null){
                state.actionState.stateAction = state.actionState.stateAction.hydrate(fleet);
                state.actionState.coroutineRunSource = fleet;
                state.actionState.run();
            }
            return fleet;
        }
        public Fleet makeFleet(Faction faction, StarNode parent, Vector3 position){
            var fleet = _makeFleet(faction,parent,position,"fleet" +  faction.state.fleets.Count);
            shipFactory.makeShip(fleet,position+new Vector3(1,0,0));
            shipFactory.makeShip(fleet,position+new Vector3(2,0,0));
            shipFactory.makeShip(fleet,position+new Vector3(3,0,0));
            parent.enterable.addFleet(fleet);
            return fleet;
        }
        private Fleet _makeFleet(Faction faction,StarNode starAt, Vector3 position,string name){
            GameObject fleetGo;
            var fleet =  makeTransforms(starAt,out fleetGo);
            var fleetState = makeFleetState(fleet,position,faction,fleetGo.transform,starAt,name);
            var fleetRenderer = makeAppearers(fleetState);
            var mover = fleetGo.AddComponent<FleetMover>().init(fleet);
            fleet.init(fleetState,fleetRenderer,mover);
            faction.state.fleets[fleet.state.id] = fleet;
            fleetGo.name = fleet.name;
            return fleet;
        }

        private Fleet makeTransforms(StarNode starAt,out GameObject go){
            go = new GameObject("fleet");
            var fleet = go.AddComponent<Fleet>();
            go.SetParent(starAt.state.positionState.appearTransform);
            return fleet;
        }
        private LinkedAppearer makeAppearers(FleetState fleetState){
            var infos = new sceneAppearInfo[sceneToPrefab.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = new sceneAppearInfo(sceneToPrefab[i]);
            }
            var mainRep = new SingleSceneAppearer( infos[3],3,fleetState.positionState);
            return new LinkedAppearer(mainRep,fleetState.shipsContainer);
        }
        private FleetState hydrateState(FleetState state, Transform appearTransform){
            state.positionState.appearTransform = appearTransform;
            state.icon = icon;
            return state;
        }
        private FleetState makeFleetState(Fleet fleet, Vector3 position,Faction faction, Transform appearTransform,StarNode star,string name){
            var positionState = new AppearableState(
                    position:position,
                    appearTransform:appearTransform,
                    star:star
                );
            return new FleetState(
                ships: new ShipsContainer(){onEmpty = ()=>{
                    Debug.Log("destroying fleet");
                    positionState.starAt.value.enterable.removeFleet(fleet);
                    GameManager.idMaker.removeObject(fleet.state.id);
                    UnityEngine.MonoBehaviour.Destroy(fleet.gameObject);
                }},
                id : GameManager.idMaker.newId(fleet),
                icon:icon,
                stamp:new FactoryStamp("fleet"),
                namedState:new Galaxy.State.NamedState(name),
                positionState: positionState,
                actionState:new SelfStateActionState(fleet),
                factionOwnedState:new FactionOwnedState{belongsTo = (Reference<Faction>)faction}
            );
        }

        // public Fleet makeFleet(Faction faction, StarNode parent, FleetModel model){
        //     if (faction.state.id != model.factionId){
        //         Debug.LogError("faction id does not match owningFaction id. Model Id:"+model.id + "  model factionId:"+model.factionId);
        //     }
        //     var fleet = makeFleet(faction,parent,model.position,model.name);
        //     foreach(var shipModel in model.shipModels){
        //         var ship = shipFactory.makeShip(fleet,shipModel);
        //     }
        //     // parent.fleetEnter(fleet);
        //     // if(model.stateAction != null){
        //     //     fleet.setStateAction(model.stateAction.hydrate(fleet));
        //     // }
        //     return fleet;
        // }
    }
}