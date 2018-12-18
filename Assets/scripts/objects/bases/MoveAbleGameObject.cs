using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using util;
using UnityEditor;

namespace Objects
{
     [System.Serializable]
 public struct SerializableVector3
 {
     public float x;
     public float y;
     public float z;
     public SerializableVector3(float rX, float rY, float rZ)
     {
         x = rX;
         y = rY;
         z = rZ;
     }
     public override string ToString()
     {
         return string.Format("[{0}, {1}, {2}]", x, y, z);
     }
     public static implicit operator Vector3(SerializableVector3 rValue)
     {
         return new Vector3(rValue.x, rValue.y, rValue.z);
     }
     public static implicit operator SerializableVector3(Vector3 rValue)
     {
         return new SerializableVector3(rValue.x, rValue.y, rValue.z);
     }
 }
    public interface ISaveAble<SeriableClass>{
        SeriableClass model{get;}
    }
    public abstract class GalaxyGameObject: MonoBehaviour,IUIable,IAppearable{
        public abstract iconInfo getIconableInfo();
        public virtual IAppearer appearer{get;set;} 
        public abstract void appear(int scene);
        public abstract IMover mover{get;}
        public Sprite icon;
    }
    public abstract class MoveAbleGameObject:GalaxyGameObject,IMoveable
    {
        public Objects.StateAction stateAction = null;
        public Objects.StateAction previousAction = null;
        public void setStateAction(StateAction action){
            
            Debug.Log("set state action " + action);
            if(this.stateAction!= null && this.stateAction.routineInstance.unityRoutine != null){
                var msg = "stop coroutine " + this.stateAction.routineInstance.unityRoutine + " routiner finished:" + this.stateAction.routineInstance.finished;
                Debug.Log(msg);
                StopCoroutine(this.stateAction.routineInstance.unityRoutine);
                previousAction = stateAction;
            }
            this.stateAction = action;
            this.stateAction.routineInstance = this.runRoutine(action);
        }

        public string title;


    }


}
