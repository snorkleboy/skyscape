using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{

    public interface IMoveable
    {
        IMover mover{get;} 
    }
    public interface IMover{
        void setTarget(Vector3 target, float stopDistence = .5f);
        Vector3 getPosition();
    }
    public class Mover :MonoBehaviour,IMover{
        public Vector3 targetVector = Vector3.negativeInfinity;
        public float distance;
        public float speed = 5f;
        public virtual Vector3 getPosition(){return transform.position;}
        public virtual void setTarget(Vector3 target,float d = .5f){
            Debug.Log("target vector  " + target);
            util.Line.DrawTempLine(transform.position,target,Color.green,4);
            targetVector = target;
            this.distance = d;
        }
        
        public virtual void Update() {
            if (!targetVector.Equals(Vector3.negativeInfinity)){
                checkDistanceAndMove();
            }
        }
        protected virtual void checkDistanceAndMove(){
            if(Vector3.Distance(targetVector, transform.position) > distance){
                moveStep();
            }
            else
            {
                targetVector = Vector3.negativeInfinity;
            }
        }
        protected virtual void moveStep(){
                float step = speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(transform.position, targetVector, step);
        }
    }
}