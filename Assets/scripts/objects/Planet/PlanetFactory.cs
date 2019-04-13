using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
using Objects.Conceptuals;
using Objects;
namespace Objects.Galaxy
{
    public class PlanetFactory: MonoBehaviour
    {
        public GameObject baseStarFab;
        public TileFactory tileFactory;
        public Sprite[] planetSprites;

        public void Start(){
            planetSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"planet");
        }

        public Planet makePlanet(StarNode star, Vector3 position)
        {
            var name = Names.planetNames.getName();
            var faction = GameManager.instance.user.faction;
            Transform parent;
            var planet = makeTransforms(star,name,out parent);
            var tileable = tileFactory.makeTileManager();

            var state = makeState(parent,planet,position,star,name,faction,tileable.state);
            var appearable = new SingleSceneAppearer(new sceneAppearInfo(baseStarFab),3,state.positionState);

            planet.Init(appearable,tileable,state);

            return planet;
        }
        public Planet makePlanet( Reference<Planet> Planetref, StarNode star,Dictionary<long,object> stateTable)
        {
            var name = Names.planetNames.getName();
            var faction = GameManager.instance.user.faction;
            Transform parent;
            var planet = makeTransforms(star,name,out parent);
            var state = (PlanetState)stateTable[Planetref.id];
            hydrateState(planet,star,state,parent,stateTable);
            var tileable = tileFactory.makeTileManager();

            var appearable = new SingleSceneAppearer(new sceneAppearInfo(baseStarFab),3,state.positionState);

            planet.Init(appearable,tileable,state);
            return planet;
        }
        private void hydrateState(Planet planet, StarNode star,PlanetState state,Transform parent, Dictionary<long,object> stateTable){
            planet.state = state;
            planet.state.positionState.appearTransform = parent;
            planet.state.positionState.starAt = star;
            planet.state.icon = planetSprites[Random.Range(0,planetSprites.Length-1)];
            GameManager.instance.factions.registerPlanetToFaction(planet,state.factionOwnedState.belongsTo.value);
            GameManager.idMaker.insertObject(planet,state.id);
            foreach (var tileRef in state.tileableState.tiles)
            {
                tileFactory.makeTile(tileRef,stateTable);
            } 
        }
        private Planet makeTransforms(StarNode starAt,string name,out Transform parent){
            var planetHolder = starAt.state.asContainerState.childrenTransform;
            var parentGo = new GameObject("planet");
            parentGo.name = name;
            parent = parentGo.transform;
            parent.SetParent(planetHolder,false);
            return parentGo.AddComponent<Planet>();
        }
        private PlanetState makeState(Transform parent,Planet planet,Vector3 position,StarNode starAt,string name,Faction faction,TileableState tileState){
            return new PlanetState(
                positionState : new State.AppearableState(
                    appearTransform:parent,
                    position:position,
                    star:starAt
                ),
                stamp:  new FactoryStamp("basic planet"),
                actionState: new SelfStateActionState(planet),
                tileableState : tileState,
                id : GameManager.idMaker.newId(planet),
                namedState : new State.NamedState(){name = name},
                icon : planetSprites[Random.Range(0,planetSprites.Length-1)],
                factionState : new State.FactionOwnedState(){belongsTo = (Reference<Faction>)GameManager.instance.factions.registerPlanetToFaction(planet,faction)}           
            );
            
        }

    }

}
