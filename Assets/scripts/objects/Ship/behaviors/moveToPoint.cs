using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects.Galaxy.ship
{
    public class MoveToPoint:StateAction{
        public Vector3 targetVector = Vector3.negativeInfinity;
        Transform controlledTransform;
        public float distance;
        public float speed;
        private LineRenderer lineRenderer;
        public MoveToPoint Init(Transform controlledTransform, float speed, float stopDistance, Vector3 targetVector){
            this.targetVector = targetVector;
            this.distance = stopDistance;
            this.speed = speed;
            this.controlledTransform = controlledTransform;
            lineRenderer = util.Line.DrawTempLine(controlledTransform.position,targetVector,Color.green,3);
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
                lineRenderer.SetPosition(0,controlledTransform.position);
                yield return null;
            }
        }
        protected bool rotateStep(){
            var direction = (targetVector - controlledTransform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            controlledTransform.rotation = Quaternion.RotateTowards(controlledTransform.rotation, lookRotation, 45*Time.deltaTime);
            float angle = Quaternion.Angle(controlledTransform.rotation, lookRotation);
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
            return Vector3.Distance(targetVector, controlledTransform.position) < distance;
        }
        protected virtual bool moveStep(){
            float step = speed * Time.deltaTime;
            this.controlledTransform.position = Vector3.MoveTowards(controlledTransform.position, targetVector, step);
            return withinDistance();
        }
        public override void Destroy(){
            if(lineRenderer){
                GameObject.Destroy(lineRenderer);
            }
        }
    }
}