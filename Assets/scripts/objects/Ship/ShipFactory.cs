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
        public Ship makeShip(ShipState shipState,Fleet fleet){
            GameObject go;
            var ship = makeTransforms(out go,fleet);
            GameManager.idMaker.insertObject(ship,shipState.id);
            hydrateState(shipState,go.transform);
            var renderer = new SingleSceneAppearer(new sceneAppearInfo(shipPrefabs[0]),3,shipState.positionState);
            var shipMover = new ShipMover().init(ship);
            ship.Init(shipState,renderer,shipMover);
            return ship;
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
        public void hydrateState(GalaxyGameObjectState state, Transform transform){
            state.icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
            state.positionState.appearTransform = transform;
        }
        private ShipState makeState(Ship ship,Transform transform,Fleet fleet,Vector3 position){
            var positionState = new State.AppearableState(
                    appearTransform:transform,
                    position:position,
                    star:fleet.state.positionState.starAt
                );
            var id =  GameManager.idMaker.newId(ship);
            return new ShipState(){
                fleetShipIsIn = fleet,
                icon =AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0],
                id=id,
                stamp= new FactoryStamp("ship"),
                namedState=new State.NamedState("ship"),
                positionState=positionState,
                factionOwnedState= fleet.state.factionOwnedState,
                actionState=new ControlledStateActionState(fleet),
                destructableState = new State.DestructableState(){hp = 100, onDestroy = ()=>{
                    Debug.Log("destroying ship");
                    fleet.state.shipsContainer.removeShip(ship);
                    GameManager.idMaker.removeObject(id);
                }},
                shieldedState = new State.ShieldedState(),
                weapons = new weapon.Weapon[1]{
                    new weapon.SimpleLaser().init(positionState, new weapon.WeaponDescription(){
                        damage = 10,
                        fireRate = .5f,
                        accuracy = 90,
                        maxDistance = 1000,
                    })
                }
            };
        }

    }


}