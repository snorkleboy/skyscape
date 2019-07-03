using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class ProtostarState : AppearableContainerState{
        public AppearablePositionState appearableState;
        [SerializeField]private List<ProtoStarConnection> _connections = new List<ProtoStarConnection>();

        public List<ProtoStarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
                appearables = new List<IAppearable>(value) ;
            }
        }
        public void addConnection(ProtoStarConnection connection)
        {
            connections.Add(connection);
            appearables.Add(connection);
        }

    }
    [System.Serializable]
    public class ProtoStar: IAppearable
    {
        public ProtostarState state;
        public IAppearer appearer { get { return starRenderer; }}
        public LinkedAppearer starRenderer;
        public void init(LinkedAppearer renderer, ProtostarState state)
        {
            this.state = state;
            this.starRenderer = renderer;
        }
    }

}
