using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    public class ShipFactory :MonoBehaviour
    {
        public Sprite[] shipIcons;
        public GameObject[] shipPrefabs;
        public void Awake()
        {
            shipPrefabs = AssetSingleton.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ship");
            if(shipPrefabs == null){
                Debug.LogError("ship factory ship prefab not found");
            }
            shipIcons =new Sprite[]{ AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"fleet")[0]};
            if(shipIcons[0] == null){
                Debug.LogWarning("ship factory did not find icon");
            }
        }
        public Ship makeShip(Fleet fleet,Vector3 position){
            GameObject go;
            var ship = makeTransforms(out go,fleet);
            var state = makeState(ship,go.transform,fleet,position);
            var renderer = new SingleSceneAppearer(new sceneAppearInfo(shipPrefabs[0]),3,state.positionState);
            var shipMover = new ShipMover().init(ship);
            ship.Init(state,renderer,shipMover);
            fleet.state.shipsContainer.addShips(ship);
            return ship;
        }
        private Ship makeTransforms(out GameObject go,Fleet fleet){
            var shipParent = fleet.state.positionState.appearTransform;
            go= new GameObject("ship");
            go.SetParent(shipParent,false);
            return go.AddComponent<Ship>();
        }
        private GalaxyGameObjectState makeState(Ship ship,Transform transform,Fleet fleet,Vector3 position){
            return new GalaxyGameObjectState(
                icon:AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0],
                id:GameManager.idMaker.newId(ship),
                stamp: new FactoryStamp("ship"),
                namedState:new State.NamedState("ship"),
                positionState:new State.AppearableState(
                    appearTransform:transform,
                    position:position,
                    star:fleet.state.positionState.starAt
                ),
                factionOwnedState: fleet.state.factionOwnedState,
                actionState:new ControlledStateActionState(fleet)
            );
        }
        public Ship makeShip(Fleet fleet, ShipModel model){
            var ship = makeShip(fleet,model.position);
            // ship.id = GameManager.idMaker.insertObject(ship,model.id);
            return ship;
        }
    }


}