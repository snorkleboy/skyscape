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
        StateAction setTarget(Vector3 target, float stopDistence = .5f);
        Vector3 getPosition();
    }
    public class Mover :MonoBehaviour, IMover{
        public float speed = 5f;
        public virtual Vector3 getPosition(){return transform.position;}
        public StateAction previousStateAction;
        public void Init(float speed = 5f){
            this.speed = speed;
        }
        public virtual StateAction setTarget(Vector3 target,float d = .5f){
            if(previousStateAction !=null){
                previousStateAction.Destroy();
            }
            return previousStateAction = new MoveTransform().Init(transform,speed,d,target);
        }

    }

    public class MoveTransform:StateAction{
        public Vector3 targetVector = Vector3.negativeInfinity;
        Transform controlledTransform;
        public float distance;
        public float speed;
        private LineRenderer lineRenderer;
        public MoveTransform Init(Transform controlledTransform, float speed, float stopDistance, Vector3 targetVector){
            this.targetVector = targetVector;
            this.distance = stopDistance;
            this.speed = speed;
            this.controlledTransform = controlledTransform;
            lineRenderer = util.Line.DrawTempLine(controlledTransform.position,targetVector,Color.green,3);
            base.Init();
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
                lineRenderer.SetPosition(0,controlledTransform.position);
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
            return Vector3.Distance(targetVector, controlledTransform.position) < distance;
        }
        protected virtual void moveStep(){
                float step = speed * Time.deltaTime;
                this.controlledTransform.position = Vector3.MoveTowards(controlledTransform.position, targetVector, step);
        }
        public override void Destroy(){
            if(lineRenderer){
                GameObject.Destroy(lineRenderer);
            }
        }
    }
}