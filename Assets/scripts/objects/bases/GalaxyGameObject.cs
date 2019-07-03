using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using util;
using UnityEditor;
using System;
using Objects.Conceptuals;
using Objects.Galaxy.State;
using Newtonsoft.Json;
namespace Objects
{
    public interface IActionable
    {
        StateActionState stateActionState { get; }
    }
    [System.Serializable]
    [DataContract]
    public class GalaxyGameObjectState : IMoveable, IActionable, IIded
    {

        public GalaxyGameObjectState() {}
        public GalaxyGameObjectState(Sprite icon, long id, FactoryStamp stamp, NamedState namedState, AppearablePositionState positionState, FactionOwnedState factionOwnedState, StateActionState actionState) {
            this.icon = icon;
            this.id = id;
            this.stamp = stamp;
            this.namedState = namedState;
            this.positionState = positionState;
            this.stateActionState = actionState;
            this.factionOwnedState = factionOwnedState;
        }
        public Sprite icon;
        [DataMember] public long id;
        public long getId() { return id; }
        [DataMember] public FactoryStamp stamp;
        [DataMember] public NamedState namedState;
        [DataMember] public AppearablePositionState positionState { get; set; }
        [DataMember] public StateActionState stateActionState { get; set; }
        [DataMember] public FactionOwnedState factionOwnedState;
    }


    [JsonObject(MemberSerialization.OptIn)]
    public abstract class baseGalaxyGameObject<StateModel>: MonoBehaviour, IHasStateObject where StateModel : IIded {
        [JsonProperty]
        public StateModel state { get; set; }

        public long getId()
        {
            if (state == null)
            {
                Debug.LogError("calling null state");
            }
            return state.getId();
        }
        public IIded stateObject { get { return state; } set { state = (StateModel)value; } }
    }

    public abstract class GalaxyGameObject<StateModel>: baseGalaxyGameObject<StateModel>, IMoveable , IActionable,IUIable, IAppearable where StateModel : IIded, IActionable, IMoveable
    {
        public AppearablePositionState positionState { get { return state.positionState; } }
        public StateActionState stateActionState { get { return state.stateActionState; } }

        public abstract IconInfo getIconableInfo();
        public virtual IAppearer appearer{get;set;} 
    }

    


}
