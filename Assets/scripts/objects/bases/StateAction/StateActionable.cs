using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using util;
using UnityEditor;
using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public abstract class StateActionState{
        public StateActionState(MonoBehaviour runSource){
            this.coroutineRunSource = runSource;
        }
        public MonoBehaviour coroutineRunSource;
        [JsonProperty]public Objects.StateAction stateAction = null;
        [JsonProperty]public Objects.StateAction previousAction = null;
        public abstract void setStateAction(StateAction action);
        public virtual void run(){
            stateAction.routineInstance = coroutineRunSource.runRoutine(stateAction);
        }
    }
    [System.Serializable]
    public class ControlledStateActionState :StateActionState{
        public ControlledStateActionState(MonoBehaviour runSource):base(runSource){}

        private StateAction temp;
        private StateAction temp2;
        public override void setStateAction(StateAction action){
            stateAction = action;
        }
        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            temp = stateAction;
            stateAction = null;
            temp2 = previousAction;
            previousAction = null;
        }
        [OnSerialized]
        internal void OnSerializedMethod(StreamingContext context)
        {
            stateAction = temp;
            temp = null;
            previousAction = temp2;
            temp2 = null;        
        }

    }
    [System.Serializable]
    public class SelfStateActionState :StateActionState{
        public SelfStateActionState(MonoBehaviour runSource):base(runSource){
        }
        private void stopPreviousAction(){
            var msg = "stop coroutine " + stateAction.routineInstance.unityRoutine + " routiner finished:" + stateAction.routineInstance.finished;
            Debug.Log(msg);
            previousAction = stateAction;
            coroutineRunSource.StopCoroutine(previousAction.routineInstance.unityRoutine);
        }
        public override void setStateAction(StateAction action){
            if(stateAction!= null && stateAction.routineInstance.unityRoutine != null){
                stopPreviousAction();
            }
            stateAction = action;
            run();
        }
    }
}