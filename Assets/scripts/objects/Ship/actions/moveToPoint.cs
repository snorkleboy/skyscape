using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Newtonsoft.Json;
namespace Objects.Galaxy.ship
{
    public class MoveToPoint:StateAction{
        [JsonProperty]public SerializableVector3 targetVector = Vector3.negativeInfinity;
        Galaxy.State.AppearableState controlledState;
        public override void hydrate<T>(T source){
            this.controlledState = source as Galaxy.State.AppearableState;
            if (this.controlledState == null){
                Debug.LogError("couldnt coerce source to AppearableState" + " " + source);
            }
            lineRenderer = util.Line.DrawTempLine(controlledState.position,targetVector,Color.green);
            base._Init();
        }
        [JsonProperty]public float distance;
        [JsonProperty]public float speed;
        private LineRenderer lineRenderer;
        public MoveToPoint Init(Galaxy.State.AppearableState controlledState, float speed, float stopDistance, Vector3 targetVector){
            this.targetVector = targetVector;
            this.distance = stopDistance;
            this.speed = speed;
            this.controlledState = controlledState;
            if(controlledState.isActive){
                lineRenderer = util.Line.DrawTempLine(controlledState.position,targetVector,Color.green,3);
            }
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            return util.Routiner.All(
                move(),
                keepLineUpdated()
            );
        }
        protected IEnumerator keepLineUpdated(){
            while(lineRenderer){
                lineRenderer.SetPosition(0,controlledState.position);
                yield return null;
            }
        }
        protected bool rotateStep(){

            var direction = (targetVector - controlledState.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            float angle = Quaternion.Angle(controlledState.rotation, lookRotation);
            // Debug.Log("direction :"+direction.y + " lookRotation:" + lookRotation.y + " controlledState.rotation" + controlledState.rotation.y);
            controlledState.rotation = Quaternion.RotateTowards(controlledState.rotation, lookRotation, 55*Time.deltaTime);
            // controlledState.rotation = Quaternion.Slerp(controlledState.rotation, lookRotation, 45*Time.deltaTime);
            angle = Quaternion.Angle(controlledState.rotation, lookRotation);
            return Mathf.Abs (angle) < 1e-3f;
        }
        protected IEnumerator move(){
            while(!rotateStep()){
                    yield return null;
            }
            while(!moveStep()){
                    yield return null;
            }
        }
        protected virtual bool withinDistance(){
            return Vector3.Distance(targetVector, controlledState.position) < distance;
        }
        protected virtual bool moveStep(){
            float step = speed * Time.deltaTime;
            this.controlledState.position = Vector3.MoveTowards(controlledState.position, targetVector, step);
            return withinDistance();
        }
        public override void Destroy(){
            if(lineRenderer){
                GameObject.Destroy(lineRenderer);
            }
        }
    }
}