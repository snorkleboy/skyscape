using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class ShipMover :BasicMover{
        public float speed = 5f;
        public virtual Vector3 getPosition(){return transform.position;}
        public override void Init(float speed = 5f){
            this.speed = speed;
        }
        public override StateAction setTarget(Vector3 target,float d = .5f){
            if(previousStateAction !=null){
                previousStateAction.Destroy();
            }
            return previousStateAction = new Objects.Galaxy.ship.MoveToPoint().Init(transform,speed,d,target);
        }

    }
}