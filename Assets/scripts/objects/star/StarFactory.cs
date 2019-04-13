using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
using Objects;
using Objects.Conceptuals;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    public class StarFactory : MonoBehaviour
    {
        [SerializeField] public GameObject[] _sceneToPrefab;
        [SerializeField] public StarConnectionFactory starConnectionFactory;
        [SerializeField] public PlanetFactory planetfactory;
        public Sprite[] starIconSprites;
        public void Start(){
            starIconSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star");
            if (starIconSprites == null){
                Debug.LogError("failed to load StarIcon Sprites");
            }
        }
        // public StarNode createStar(Transform holder, StarNodeModel model){
        //     var star = initBaseStar(holder,model.position);//model.id
        //     // star.state.namedState.name = model.name;
        //     // star.gameObject.name = model.name;
        //     // foreach(var connection in model.starConnections){
        //     //     var otherId = model.id == connection.starIds[0]? connection.starIds[1] : connection.starIds[0];
        //     //     var starRef = new StarNode(otherId);
        //     //     makeConnection(star,starRef);
        //     // }
        //     // var planets = new Reference<Planet>[model.planets.Length];
        //     // var count = 0;
        //     // foreach(var planetModel in model.planets){
        //     //     planets[count++] = new Reference<Planet>(planetfactory.makePlanet(star,planetModel));
        //     // }
        //     // star.state.asContainerState.setPlanets(planets);

        //     // foreach(var fleetModel in model.fleets){
        //     //     var faction = fleetModel.factionId.dereference<Faction>();
        //     //     var fleet = faction.createFleet(fleetModel,star);
        //     // }
        //     return star;
        // }
        public StarNode createStar(Transform holder, Vector3 position){
            Transform representationTransform;
            Transform ChildrenTransform;

            var star = makeTransforms(holder,out representationTransform,out ChildrenTransform);
            var starState = makeBaseState(star,position,representationTransform,ChildrenTransform,name);
            var appearer = makeAppearer(starState.asContainerState,starState.positionState);
            star.Init(appearer,starState);
            return star;
        }
        public IEnumerator createStar(Transform holder,Reference<StarNode> refStarNode,Dictionary<long,object> stateTable,System.Action<StarNode> onCreate){
            Transform representationTransform;
            Transform ChildrenTransform;
            yield return null;
            var star = makeTransforms(holder,out representationTransform,out ChildrenTransform); 
            var state = (StarNodeState)stateTable[refStarNode.id];
            yield return hydrateState(star,state,representationTransform,ChildrenTransform,stateTable);
            var appearer = makeAppearer(state.asContainerState,state.positionState);
            star.Init(appearer,state);
            Debug.Log("created star " + refStarNode.value.state.id);
            onCreate(star);
        }


        private StarNode makeTransforms(Transform holder,out Transform representationTransform,out Transform childrenTransform){
            var starGo = new GameObject("starNode");
            var star = starGo.AddComponent<StarNode>();
            star.transform.SetParent(holder);
            var childrenHolder = new GameObject("children");
            childrenHolder.transform.SetParent(starGo.transform);
            childrenTransform = childrenHolder.transform;
            var representation = new GameObject("representation");
            representation.transform.SetParent(star.transform.transform);
            representationTransform = representation.transform;
            return star;
        }
        public IEnumerator hydrateState(StarNode starter, StarNodeState state,Transform representationTransform,Transform ChildrenTransform,Dictionary<long,object> stateTable){
            starter.state = state;
            GameManager.idMaker.insertObject(starter,state.id);
            state.positionState.appearTransform = representationTransform;
            state.asContainerState.childrenTransform = ChildrenTransform;
            state.icon =  starIconSprites[0];
            foreach (var connectionRef in state.asContainerState.connections)
            {
                var connection = starConnectionFactory.makeConnection(starter,connectionRef,stateTable);
                state.asContainerState.appearables.Add(connection);
                Debug.Log("created star connection  " + connectionRef.value.state.id);
            } 
            foreach (var planetRef in state.asContainerState.planets)
            {
                var planet = planetfactory.makePlanet(planetRef,starter,stateTable);
                state.asContainerState.appearables.Add(planet);
                Debug.Log("created planet " + planetRef.value.state.id);
                yield return null;
            } 
        }
        private StarNodeState makeBaseState(StarNode node,Vector3 position,Transform representationTransform,Transform childrenTransform, string name){
            return new StarNodeState(
                positionState: new AppearableState(appearTransform:representationTransform,position:position,star:null,isActive:false),
                asContainerState : new StarAsContainerState(childrenTransform),
                actionState : new SelfStateActionState(node),
                namedState : new NamedState(Names.starNames.getName()),
                stamp : new FactoryStamp("basic star"),
                id : GameManager.idMaker.newId(node),
                icon : starIconSprites[0],
                factionOwned:new FactionOwnedState()
            );
        }
        private LinkedAppearer makeAppearer(AppearableContainerState containerState, AppearableState appearableState){
            var infos = new sceneAppearInfo[_sceneToPrefab.Length];
            for (int i = 0; i < _sceneToPrefab.Length; i++)
            {
                infos[i] = new sceneAppearInfo(_sceneToPrefab[i]);;
            }
            infos[3] = new sceneAppearInfo(_sceneToPrefab[3],Vector3.zero);;
            var mainrep = new MultiSceneAppearer(infos,appearableState);
            return new LinkedAppearer(mainrep,containerState);
        }
        public virtual StarConnection makeConnection(StarNode a, StarNode b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}



