using System;
using UnityEngine;
using UnityEditor;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    public class StarConnectionModel{
        public StarConnectionModel(){}

        public StarConnectionModel(StarConnection connection){
            starIds = new long[connection.state.nodes.Length];
            for(var i=0;i<starIds.Length;i++)
            {
                starIds[i] = connection.state.nodes[i].getId();
            }
        }
        public long[] starIds;
    }
    [Serializable]
    public class StarConnectionState
    {        
        public double strength;
        public Reference<StarNode>[] nodes;
        public AppearableState appearableState;
    }
    public class StarConnection: MonoBehaviour,IAppearable, ISaveAble<StarConnectionModel>
    {
        public StarConnectionModel model{get{return new StarConnectionModel(this);}}
        public IAppearer appearer { get; set; }
        public StarConnectionState state;

        public void Init(StarConnectionState state, StarConnectionAppearer renderer)
        {
            appearer = renderer;
            this.state = state;
        }

    }


}
