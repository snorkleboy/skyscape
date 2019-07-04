using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    public enum shipTypes{
        fighter,
        carrier,
        frigate
    }
    public class ShipFactory :MonoBehaviour
    {
        public Sprite[] shipIcons;
        public GameObject[] shipPrefabs;
        public void Awake()
        {
            shipPrefabs = AssetSingleton.getBundledDirectory<GameObject>(AssetSingleton.bundleNames.prefabs,"ship");
            if (shipPrefabs == null){
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
            hydrateState(shipState,go.transform,fleet,ship);
            var renderer = new SingleSceneAppearer(new sceneAppearInfo(shipPrefabs[0]),3,shipState.positionState);
            ship.Init(shipState,renderer);
            return ship;
        }

        public Ship makeShip(Fleet fleet,Vector3 position){
            GameObject go;
            var ship = makeTransforms(out go,fleet);
            var state = makeState(ship,go.transform,fleet,position);
            var renderer = new SingleSceneAppearer(new sceneAppearInfo(shipPrefabs[0]),3,state.positionState);
            ship.Init(state,renderer);
            fleet.state.shipsContainer.addShips(ship);
            return ship;
        }
        private Ship makeTransforms(out GameObject go,Fleet fleet){
            var shipParent = fleet.state.positionState.appearTransform;
            go= new GameObject("ship");
            go.SetParent(shipParent,false);
            return go.AddComponent<Ship>();
        }
        public void hydrateState(ShipState state, Transform transform,Fleet fleet,Ship ship){
            state.icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
            state.positionState.appearTransform = transform;
            state.weapons[0].init(state.positionState,state.weapons[0].weaponDescription);
            state.destructableState.onDestroy = ()=>onDestroy(state.id,ship,fleet);
        }
        private void onDestroy(long id,Ship ship,Fleet fleet){
            Debug.Log("destroying ship :" + id);
            fleet.state.shipsContainer.removeShip(ship);
            UnityEngine.MonoBehaviour.Destroy(ship.gameObject);
            GameManager.idMaker.removeObject(id);
        }
        private ShipState makeState(Ship ship,Transform transform,Fleet fleet,Vector3 position){
            var positionState = new State.AppearablePositionState(
                    appearTransform:transform,
                    position:position,
                    star:fleet.state.positionState.starAt
                );
            var id =  GameManager.idMaker.newId(ship);
            return new ShipState(){
                fleetShipIsIn = fleet,
                icon =AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0],
                id=id,
                stamp= new FactoryStamp(shipTypes.fighter),
                namedState=new State.NamedState("ship"),
                positionState=positionState,
                factionOwnedState= fleet.state.factionOwnedState,
                stateActionState=new ControlledStateActionState(fleet),
                destructableState = new State.DestructableState(){hp = 100,onDestroy=()=>onDestroy(id,ship,fleet) },
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