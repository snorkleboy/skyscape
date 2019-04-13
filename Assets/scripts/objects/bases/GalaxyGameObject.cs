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
    public interface IStateFul<T>{
        T state{get;}
    }

    [System.Serializable]
    [DataContract]
    public class GalaxyGameObjectState{
        public GalaxyGameObjectState(){}
        public GalaxyGameObjectState(Sprite icon,long id,FactoryStamp stamp,NamedState namedState,AppearableState positionState,FactionOwnedState factionOwnedState,StateActionState actionState){
            this.icon = icon;
            this.id = id;
            this.stamp = stamp;
            this.namedState = namedState;
            this.positionState = positionState;
            this.actionState = actionState;
            this.factionOwnedState = factionOwnedState;
        }
        public Sprite icon;
        [DataMember]public long id;
        [DataMember]public FactoryStamp stamp;
        [DataMember]public NamedState namedState;

        [DataMember]public AppearableState positionState;
        [DataMember]public StateActionState actionState;
        [DataMember]public FactionOwnedState factionOwnedState;

    }

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class GalaxyGameObject<StateModel>:MonoBehaviour,IUIable,IAppearable,ISaveable<StateModel> where StateModel:GalaxyGameObjectState
    {
        [JsonProperty]
        public StateModel state{get;set;}
        public object stateObject{get{return state;}set{state = (StateModel)value;}}
        public long getId(){
            if(state == null){
                Debug.LogError("calling null state");
            }
            return state.id;
        }
        public abstract IconInfo getIconableInfo();
        public virtual IAppearer appearer{get;set;} 
    }

    


}
