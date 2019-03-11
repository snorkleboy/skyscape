using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Galaxy.State;

namespace Objects
{
    public interface IMoveable
    {
        IMover mover{get;} 
    }
    public interface IMover{
        StateAction moveTo(Vector3 target, float stopDistence = .5f);
        AppearableState appearableState{get;}
        StateActionState stateActionState{get;}
    }

    public abstract class BasicMover :MonoBehaviour, IMover{
        public float speed = 5f;
        public StateActionState stateActionState{get;set;}
        public AppearableState appearableState{get;set;}
        public virtual void init(float speed = 5f){
            this.speed = speed;
        }        
        public abstract StateAction moveTo(Vector3 target,float d = .5f);

    }

}