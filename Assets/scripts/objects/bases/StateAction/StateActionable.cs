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
    public class StateActionState{
        public StateActionState(MonoBehaviour runSource){
            this.coroutineRunSource = runSource;
        }
        public MonoBehaviour coroutineRunSource;
        [JsonProperty]public Objects.StateAction stateAction = null;
        [JsonProperty]public Objects.StateAction previousAction = null;
        public virtual void setStateAction(StateAction action){
           throw(new NotImplementedException());
        }
        public virtual void run(){
            stateAction.routineInstance = coroutineRunSource.runRoutine(stateAction);
        }
    }
    [System.Serializable]
    public class ControlledStateActionState :StateActionState{
        public ControlledStateActionState(MonoBehaviour runSource):base(runSource){}
        public override void setStateAction(StateAction action){
            Debug.Log("set state action (controlled)" + action);
            stateAction = action;
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
            Debug.Log("set state action (self controlled)" + action);
            if(stateAction!= null && stateAction.routineInstance.unityRoutine != null){
                stopPreviousAction();
            }
            stateAction = action;
            run();
        }
    }
}