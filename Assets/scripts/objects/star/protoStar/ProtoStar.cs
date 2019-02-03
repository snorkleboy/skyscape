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
    public class ProtoStar:IAppearable
    {
        public IAppearer appearer { get { return starRenderer; } 
            set
            {
                starRenderer = (LinkedAppearer)value;
            }
        }
        public LinkedAppearer starRenderer;
        public Transform transform { get { return appearer.activeGO.transform; } }
        public Vector3 position;

        [SerializeField] private List<ProtoStarConnection> _connections = new List<ProtoStarConnection>();
        public ProtoStar(LinkedAppearer renderer)
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
                starRenderer.setAppearables(_connections.ToArray());
            }
        }
        public void addConnection(ProtoStarConnection connection)
        {
            connections.Add(connection);
            starRenderer.addAppearables(connections.ToArray());
        }
        public void appear(int scene)
        {
            starRenderer.appear(scene);//,position
            starRenderer.activeGO.transform.position = position;
        }

    }

}
