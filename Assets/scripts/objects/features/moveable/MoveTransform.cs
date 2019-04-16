using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
        public class MovePositionState:StateAction{
        public Vector3 targetVector = Vector3.negativeInfinity;
        Objects.Galaxy.State.AppearableState controlledState;

        public override void hydrate<T>(T source){
            this.controlledState = source as Objects.Galaxy.State.AppearableState;
            if (this.controlledState == null){
                Debug.LogError("couldnt coerce source to AppearableState" + " " + source);
            }
            base._Init();
        }
        public float distance;
        public float speed;
        private LineRenderer lineRenderer;
        public MovePositionState Init(Objects.Galaxy.State.AppearableState controlledState, float speed, float stopDistance, Vector3 targetVector){
            this.targetVector = targetVector;
            this.distance = stopDistance;
            this.speed = speed;
            this.controlledState = controlledState;
            lineRenderer = util.Line.DrawTempLine(controlledState.position,targetVector,Color.green,3);
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            return util.Routiner.All(
                main(),
                keepLineUpdated()
            );
        }
        protected IEnumerator keepLineUpdated(){
            while(lineRenderer){
                lineRenderer.SetPosition(0,controlledState.position);
                yield return null;
            }
        }
        protected IEnumerator main(){
            while(!checkExitCondition()){
                moveStep();
                yield return null;
            }
        }
        protected virtual bool checkExitCondition(){
            return Vector3.Distance(targetVector, controlledState.position) < distance;
        }
        protected virtual void moveStep(){
                float step = speed * Time.deltaTime;
                this.controlledState.position = Vector3.MoveTowards(controlledState.position, targetVector, step);
        }
        public override void Destroy(){
            if(lineRenderer){
                GameObject.Destroy(lineRenderer);
            }
        }
    }
}