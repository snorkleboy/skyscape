using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class StarNode : IRenderable
    {
        public IRenderer renderHelper { get { return starRenderer; } }
        public HolderRenderer<StarNode> starRenderer;
        public Transform transform { get { return renderHelper.transform; } }
        public Vector3 position;
        [SerializeField] private List<StarConnection> _connections;
        public List<StarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
                starRenderer.addRenderables(_connections.ToArray());
            }
        }
        public void addConnection(StarConnection connection)
        {
            connections.Add(connection);
            starRenderer.addRenderables(connections.ToArray());
        }
        [SerializeField] private Planet[] _planets;
        public Planet[] planets {
            get
            {
                return _planets;
            }
            set
            {
                _planets = value;
                starRenderer.addRenderables(_planets);
            }
        }
        public StarNode(HolderRenderer<StarNode> renderer)
        {
            this.starRenderer = renderer;
            renderer.scriptSingelton = this;
            connections = new List<StarConnection>();
        }

        public void render(int scene)
        {
            starRenderer.render(scene, position);
            if (scene == 3){
                starRenderer.transform.position = Vector3.zero;
            }else{
                starRenderer.transform.position = position;
            }
        }

    }

}
