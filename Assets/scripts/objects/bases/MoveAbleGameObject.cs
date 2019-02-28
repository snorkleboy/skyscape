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
        public Sprite icon;
        public long id;
        public FactoryStamp stamp;

        public NamedState namedState;
        public FactionOwnedState factionOwnedState;
        public AppearableState positionState;

    }

    public abstract class GalaxyGameObject<StateModel>: MonoBehaviour,IUIable,IAppearable,IIded where StateModel:GalaxyGameObjectState
    {
        public long getId(){
            return state.id;
        }
        public StateModel state{get;set;}
        public abstract IconInfo getIconableInfo();
        public abstract IAppearer appearer{get;} 
    }
    public abstract class MoveAbleGameObject<StateModel>:GalaxyGameObject<StateModel>,IStateActionable,IMoveable where StateModel:GalaxyGameObjectState
    {
        public IStateActionManager stateActionManager{get;}
        public abstract IMover mover{get;}
    }
    


}
