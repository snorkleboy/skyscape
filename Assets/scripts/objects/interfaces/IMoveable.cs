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
        public GameObject targetGO = null;
        public float distance;
        public float speed = 1f;
        public void setFollowTarget(GameObject gameObject, float d = .5f){
            this.distance = d;
            targetVector = gameObject.transform.position;
            targetGO = gameObject;
        }
        public void setTarget(Vector3 target,float d = .5f){
            Debug.Log("target vector  " + target);
            targetVector = target;
            this.distance = d;
        }
        
        public void Update() {
            if (targetGO != null){
                targetVector = targetGO.transform.position;
            }
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
                targetGO = null;
            }
        }
        private void moveStep(){
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetVector, step);
        }
    }
}