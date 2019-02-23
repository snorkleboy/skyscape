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
    using UnityEngine;
 using System;
 using System.Collections;
 
 /// <summary>
 /// Since unity doesn't flag the Quaternion as serializable, we
 /// need to create our own version. This one will automatically convert
 /// between Quaternion and SerializableQuaternion
 /// </summary>
 [System.Serializable]
 public struct SerializableQuaternion
 {
     /// <summary>
     /// x component
     /// </summary>
     public float x;
     
     /// <summary>
     /// y component
     /// </summary>
     public float y;
     
     /// <summary>
     /// z component
     /// </summary>
     public float z;
     
     /// <summary>
     /// w component
     /// </summary>
     public float w;
     
     /// <summary>
     /// Constructor
     /// </summary>
     /// <param name="rX"></param>
     /// <param name="rY"></param>
     /// <param name="rZ"></param>
     /// <param name="rW"></param>
     public SerializableQuaternion(float rX, float rY, float rZ, float rW)
     {
         x = rX;
         y = rY;
         z = rZ;
         w = rW;
     }
     
     /// <summary>
     /// Returns a string representation of the object
     /// </summary>
     /// <returns></returns>
     public override string ToString()
     {
         return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
     }
     
     /// <summary>
     /// Automatic conversion from SerializableQuaternion to Quaternion
     /// </summary>
     /// <param name="rValue"></param>
     /// <returns></returns>
     public static implicit operator Quaternion(SerializableQuaternion rValue)
     {
         return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
     }
     
     /// <summary>
     /// Automatic conversion from Quaternion to SerializableQuaternion
     /// </summary>
     /// <param name="rValue"></param>
     /// <returns></returns>
     public static implicit operator SerializableQuaternion(Quaternion rValue)
     {
         return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
     }
 }
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
                stopPreviousAction();
            }
            this.stateAction = action;
            this.stateAction.routineInstance = this.runRoutine(action);
        }
        private void stopPreviousAction(){
                var msg = "stop coroutine " + this.stateAction.routineInstance.unityRoutine + " routiner finished:" + this.stateAction.routineInstance.finished;
                Debug.Log(msg);
                previousAction = stateAction;
                StopCoroutine(previousAction.routineInstance.unityRoutine);
        }
        public string title;
    }


}
