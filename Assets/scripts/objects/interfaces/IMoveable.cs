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
    }
    public class Mover :MonoBehaviour,IMover{
        public Vector3 targetVector = Vector3.negativeInfinity;
        public float distance;
        public float speed = 5f;

        public void setTarget(Vector3 target,float d = .5f){
            Debug.Log("target vector  " + target);
            targetVector = target;
            this.distance = d;
        }
        
        public void Update() {
            if (!targetVector.Equals(Vector3.negativeInfinity)){
                checkDistanceAndMove();
            }
        }
        private void checkDistanceAndMove(){
            if(Vector3.Distance(targetVector, transform.position) > distance){
                moveStep();
            }
            else
            {
                targetVector = Vector3.negativeInfinity;
            }
        }
        private void moveStep(){
                float step = speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(transform.position, targetVector, step);
        }
    }
}