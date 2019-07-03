using System;
using UnityEngine;
using UnityEditor;
using Objects.Galaxy.State;
using System.Linq;
using Newtonsoft.Json;
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
    public class StarConnectionState:IIded
    {        
        public double strength;
        public Reference<StarNode>[] nodes;
        public long id;
        public long getId() { return id; }

        public AppearablePositionState appearableState;
    }
    [JsonObject(MemberSerialization.OptIn)]

    public class StarConnection: MonoBehaviour,IAppearable,IHasStateObject, IEquatable<StarConnection>
    {
        public IAppearer appearer { get; set; }
        public long getId(){return state.id;}

        [JsonProperty]public StarConnectionState state {get;set;}
        public IIded stateObject {get{return state;}set{state = (StarConnectionState)value;}}

        public void Init(StarConnectionState state, StarConnectionAppearer renderer)
        {
            appearer = renderer;
            this.state = state;
        }
        public bool Equals(StarConnection other){
            var theseNodes = state.nodes;
            var otherIdA = other.state.nodes[0].getId();
            var otherIdB = other.state.nodes[1].getId();
            var thisIdA = state.nodes[0].getId();
            var thisIdB = state.nodes[1].getId();
            var hasA = thisIdA == otherIdA || thisIdA == otherIdB;
            var hasB = thisIdB == otherIdA || thisIdB == otherIdB;
            return hasA && hasB;
        }

    }


}
