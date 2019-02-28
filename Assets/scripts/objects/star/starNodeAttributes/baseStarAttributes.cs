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
            var starAt = fleet.state.positionState.starAt;
            starAt.state.asContainerState.removeFleet(fleet);
            to.state.asContainerState.addFleet(fleet);            
        }
    }
    public class StarAsContainerState:AppearableContainerState{
        public StarAsContainerState(Transform childTransform):base(childTransform)
        {

        }
        public List<Reference<Planet>> planets = new List<Reference<Planet>>();
        public List<Reference<Fleet>> fleets = new List<Reference<Fleet>>();
        public List<StarConnection> connections = new List<StarConnection>();

        public StarConnection getConnection(long id){
            foreach(var connection in connections){
                if(connection.nodes[0].getId() == id || connection.nodes[1].getId() == id){
                    return connection;
                }
            }
            return null;
        }
        public void addConnection(StarConnection connection)
        {
            bool alreadyAdded = connections.Any(existingConnection=>(
                existingConnection.nodes.Any(node=>node.getId() == existingConnection.nodes[0].getId()) && 
                existingConnection.nodes.Any(node=>node.getId() == existingConnection.nodes[1].getId())
            ));
            if (!alreadyAdded){
                addConnection(connection);
                addAppearable(connection);
            }
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