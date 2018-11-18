using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class ProtoStar:IRenderable
    {
        public IRenderer renderHelper { get { return starRenderer; } 
            set
            {
                starRenderer = (HolderRenderer<ProtoStar>)value;
            }
        }
        public HolderRenderer<ProtoStar> starRenderer;
        public Transform transform { get { return renderHelper.transform; } }
        public Vector3 position;

        [SerializeField] private List<ProtoStarConnection> _connections = new List<ProtoStarConnection>();
        public ProtoStar(HolderRenderer<ProtoStar> renderer)
        {
            this.starRenderer = renderer;
        }
        public List<ProtoStarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
                starRenderer.setRenderables(_connections.ToArray());
            }
        }
        public void addConnection(ProtoStarConnection connection)
        {
            connections.Add(connection);
            starRenderer.addRenderables(connections.ToArray());
        }
        public void render(int scene)
        {
            starRenderer.render(scene, position);
            starRenderer.transform.position = position;
        }

    }

}
