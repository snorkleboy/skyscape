using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public abstract class BasicMover :MonoBehaviour, IMover{
        public float speed = 5f;
        public virtual Vector3 getPosition(){return transform.position;}
        public StateAction previousStateAction;
        public abstract void Init(float speed = 5f);
        public abstract StateAction setTarget(Vector3 target,float d = .5f);

    }
    public class Mover :BasicMover{
        public override Vector3 getPosition(){return transform.position;}
        public override void Init(float speed = 5f){
            this.speed = speed;
        }
        public override StateAction setTarget(Vector3 target,float d = .5f){
            if(previousStateAction !=null){
                previousStateAction.Destroy();
            }
            return previousStateAction = new MoveTransform().Init(transform,speed,d,target);
        }
    }
}