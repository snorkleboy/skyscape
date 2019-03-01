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

namespace Objects
{

    public interface ISaveAble<SerializableClass>{
        SerializableClass model{get;}
    }
    
    public class GalaxyGameObjectState{
        public GalaxyGameObjectState(){}
        public GalaxyGameObjectState(Sprite icon,long id,FactoryStamp stamp,NamedState namedState,AppearableState positionState){
            this.icon = icon;
            this.id = id;
            this.stamp = stamp;
            this.namedState = namedState;
            this.positionState = positionState;
        }
        public Sprite icon;
        public long id;
        public FactoryStamp stamp;
        public NamedState namedState;
        public AppearableState positionState;

    }

    public abstract class GalaxyGameObject<StateModel>: MonoBehaviour,IUIable,IAppearable,IIded where StateModel:GalaxyGameObjectState
    {
        public long getId(){
            return state.id;
        }
        //todo move model stuff into state and make state private-ish
        public StateModel state{get;set;}
        public abstract IconInfo getIconableInfo();
        public virtual IAppearer appearer{get;} 
    }
    public abstract class MoveAbleGameObject<StateModel>:GalaxyGameObject<StateModel>,IStateActionable,IMoveable where StateModel:GalaxyGameObjectState
    {
        public IStateActionManager stateActionManager{get;}
        public virtual IMover mover{get;}
    }
    


}
