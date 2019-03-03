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

namespace Objects.Galaxy
{

    public static class StarContainerExtension{
        public static void moveToStar(this Fleet fleet, StarNode to){
            var starAt = fleet.appearer.state.starAt;
            starAt.enterable.removeFleet(fleet);
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
        public void setPlanets(Reference<Planet>[] planets) {
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

    public class StarAsContainerState:AppearableContainerState{
        public StarAsContainerState(Transform childTransform):base(childTransform)
        {

        }
        public List<Reference<Planet>> planets = new List<Reference<Planet>>();
        public List<Reference<Fleet>> fleets = new List<Reference<Fleet>>();
        public List<StarConnection> connections = new List<StarConnection>();

        public StarConnection getConnection(long id){
            foreach(var connection in connections){
                if(connection.state.nodes[0].getId() == id || connection.state.nodes[1].getId() == id){
                    return connection;
                }
            }
            return null;
        }
        public void addConnection(StarConnection connection)
        {
            bool alreadyAdded = connections.Any(existingConnection=>existingConnection.Equals(connection));
        if (!alreadyAdded){
                connections.Add(connection);
                addAppearable(connection);
            }
            Debug.Log("connections count:"+connections.Count + " " + alreadyAdded);

        }
        public void setPlanets(Reference<Planet>[] planets) {
            this.planets = planets.ToList();
            addAppearable(planets.getAllReferenced());
        }
        public void addFleet(Fleet fleet){
            fleets.Add(new Reference<Fleet>(fleet));
            addAppearable(fleet);
        }
        public void removeFleet(Fleet fleet){
            fleets.Remove(new Reference<Fleet>(fleet));
            removeAppearable(fleet);
        }

    }

}