using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using UnityEditor;
namespace Objects.Galaxy
{
    public class Spaceable: ScriptableObject{
        public List<Reference<Planet>> planets;
        public List<Reference<Fleet>> fleets;
    }

    public class Connectable : ScriptableObject{
        [SerializeField] List<StarConnection> _connections = new List<StarConnection>();
        public List<StarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
            }
        }
        public StarConnection getConnection(long id){
            foreach(var connection in _connections){
                if(connection.nodes[0].getId() == id || connection.nodes[1].getId() == id){
                    return connection;
                }
            }
            return null;
        }
        public void addConnection(StarConnection connection)
        {
            connections.Add(connection);
        }
    }
}