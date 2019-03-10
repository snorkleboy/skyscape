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
        StateAction setTarget(Vector3 target, float stopDistence = .5f);
        AppearableState appearableState{get;}
        StateAction StateAction{get;}
    }

    public abstract class BasicMover :MonoBehaviour, IMover{
        public float speed = 5f;
        public StateAction StateAction{get;set;}
        public AppearableState appearableState{get;set;}
        public virtual void init(float speed = 5f){
            this.speed = speed;
        }        
        public abstract StateAction setTarget(Vector3 target,float d = .5f);

    }
    public class Mover :BasicMover{
        public override StateAction setTarget(Vector3 target,float d = .5f){
            if(StateAction !=null){
                StateAction.Destroy();
            }
            return StateAction = new MoveTransform().Init(transform,speed,d,target);
        }
    }
}