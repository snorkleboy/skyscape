using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using util;
using UnityEditor;
 using System;
namespace Objects
{

    public class StateActionState{
        public StateActionState(MonoBehaviour runSource){
            this.coroutineRunSource = runSource;
        }
        public Objects.StateAction stateAction = null;
        public Objects.StateAction previousAction = null;
        public MonoBehaviour coroutineRunSource;

        public void setStateAction(StateAction action){
            Debug.Log("set state action " + action);
            if(stateAction!= null && stateAction.routineInstance.unityRoutine != null){
                stopPreviousAction();
            }
            stateAction = action;
            stateAction.routineInstance = coroutineRunSource.runRoutine(action);
        }
        private void stopPreviousAction(){
            var msg = "stop coroutine " + stateAction.routineInstance.unityRoutine + " routiner finished:" + stateAction.routineInstance.finished;
            Debug.Log(msg);
            previousAction = stateAction;
            coroutineRunSource.StopCoroutine(previousAction.routineInstance.unityRoutine);
        }
    }

}