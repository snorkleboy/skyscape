using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Newtonsoft.Json;
namespace Objects.Galaxy.ship
{
    public class FollowReference:StateAction{
        [JsonProperty]public Galaxy.State.AppearablePositionState target;
        Galaxy.State.AppearablePositionState controlledState;
        float distance;
        float speed;
        bool exitOnClose;
        public FollowReference Init(Galaxy.State.AppearablePositionState controlledState,Galaxy.State.AppearablePositionState targetState, float speed, float stopDistance,bool exitOnClose = false){
            this.target = targetState;
            this.distance = stopDistance;
            this.speed = speed;
            this.controlledState = controlledState;
            this.exitOnClose = exitOnClose;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            return move();
        }
        protected bool shouldExit()
        {
            return exitOnClose && withinDistance();
        }
        protected IEnumerator move(){
            while(!shouldExit())
            {
                while(!rotateStep())
                {
                    yield return null;
                }
                while(!moveStep()){
                    yield return null;
                }
                yield return null;
            }

        }
        protected bool rotateStep(){

            var direction = (target.position - controlledState.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            float angle = Quaternion.Angle(controlledState.rotation, lookRotation);
            // Debug.Log("direction :"+direction.y + " lookRotation:" + lookRotation.y + " controlledState.rotation" + controlledState.rotation.y);
            controlledState.rotation = Quaternion.RotateTowards(controlledState.rotation, lookRotation, 55*Time.deltaTime);
            // controlledState.rotation = Quaternion.Slerp(controlledState.rotation, lookRotation, 45*Time.deltaTime);
            angle = Quaternion.Angle(controlledState.rotation, lookRotation);
            return Mathf.Abs (angle) < 1e-3f;
        }

        protected virtual bool withinDistance(){
            var dist = Vector3.Distance(target.position, controlledState.position);
            return dist < distance;
        }
        protected virtual bool moveStep(){
            var withinDistanceBool = withinDistance();
            if(!withinDistanceBool){
                float step = speed * Time.deltaTime;
                this.controlledState.position = Vector3.MoveTowards(controlledState.position, target.position, step);
                return withinDistance();
            }
            return withinDistanceBool;
        }

    }
}