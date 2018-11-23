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
        Mover mover{get;set;} 
    }
    public class Mover :MonoBehaviour{
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
                Debug.Log("distence " + Vector3.Distance(targetVector, transform.position) + " transform.position" + transform.position + " targetVector "+  targetVector + " goName" + gameObject.name);
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