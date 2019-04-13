using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using System.Linq;
using UnityEditor;
using Objects.Galaxy.State;
using Newtonsoft.Json;

namespace Objects.Galaxy
{

    public static class StarContainerExtension{
        public static void moveToStar(this Fleet fleet, StarNode to){
            var starAt = fleet.appearer.state.starAt;
            starAt.value.enterable.removeFleet(fleet);
            to.enterable.addFleet(fleet);            
        }
    }
    public class EnterableStar{
        public EnterableStar(StarAsContainerState state){
            this.state = state;
        }
        StarAsContainerState state;
        public StarConnection getConnection(long id){
            return state.getConnection(id);
        }
        public void addConnection(StarConnection connection){
            state.addConnection(connection);
        }
        public void setPlanets(Planet[] planets) {
            state.setPlanets(planets);
        }
        public void addFleet(Fleet fleet){
            state.addFleet(fleet);
        }
        public void removeFleet(Fleet fleet){
            state.removeFleet(fleet);
        }

    }
    [System.Serializable]
    public partial class StarAsContainerState:AppearableContainerState{
        public StarAsContainerState()
        {

        }
        public StarAsContainerState(Transform childTransform):base(childTransform)
        {

        }
        public List<Reference<Planet>> planets = new List<Reference<Planet>>();
        public List<Reference<Fleet>> fleets = new List<Reference<Fleet>>();
        [JsonProperty]

        public List<Reference<StarConnection>> connections = new List<Reference<StarConnection>>();

        public StarConnection getConnection(long id){
            foreach(var connection in connections){
                if(connection.value.state.nodes[0].getId() == id || connection.value.state.nodes[1].getId() == id){
                    return connection;
                }
            }
            return null;
        }
        public void addConnection(StarConnection connection)
        {
            bool alreadyAdded = connections.Any(existingConnection=>existingConnection.Equals(connection));
            if (!alreadyAdded){
                connections.Add((Reference<StarConnection>)connection);
                addAppearable(connection);
            }

        }
        public void setPlanets(Planet[] planets) {
            this.planets = planets.referenceAll();
            addAppearable(planets);
        }
        public void addFleet(Fleet fleet){
            fleets.Add((Reference<Fleet>)fleet);
            addAppearable(fleet);
        }
        public void removeFleet(Fleet fleet){
            fleets.Remove((Reference<Fleet>)fleet);
            removeAppearable(fleet);
        }

    }
    // public partial class StarAsContainerState{

    //     public List<PlanetState> _planetStates = new List<PlanetState>();
    //     // [JsonProperty]
    //     public List<PlanetState> planetStates{
    //         get{
    //             if(_planetStates == null){
    //                 _planetStates = planets.Select(i=>i.state).ToList();
    //             }
    //             return _planetStates;
    //         }
    //         set{_planetStates = value;}
    //     }

    //     public List<FleetState> _fleetStates = new List<FleetState>();
    //     // [JsonProperty]

    //     public List<FleetState> fleetStates{
    //         get{
    //             if(_fleetStates == null){
    //                 _fleetStates = fleets.Select(i=>i.state).ToList();
    //             }
    //             return _fleetStates;
    //         }
    //         set{_fleetStates = value;}
    //     }



    //     private List<StarConnectionState> _connectionStates;
    //     [JsonProperty]
    //     private List<StarConnectionState> connectionStates{
    //         get{
    //             if(_connectionStates == null){
    //                 _connectionStates = connections.Select(i=>i.state).ToList();
    //             }
    //             return _connectionStates;
    //         }
    //         set{_connectionStates = value;}
    //     }
    // }
}