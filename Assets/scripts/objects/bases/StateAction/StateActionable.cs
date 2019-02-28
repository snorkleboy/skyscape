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
    public interface IStateActionable{
        IStateActionManager stateActionManager{get;}
    }

    public class StateActionState{
        public Objects.StateAction stateAction = null;
        public Objects.StateAction previousAction = null;
        public MonoBehaviour coroutineRunSource;
    }
    public interface IStateActionManager{
        StateActionState state{get;}
        void setStateAction(StateAction action);
    }
    public class StateActionManager:IStateActionManager{
        public StateActionState state{get;set;}
        public void setStateAction(StateAction action){
            Debug.Log("set state action " + action);
            if(state.stateAction!= null && state.stateAction.routineInstance.unityRoutine != null){
                stopPreviousAction();
            }
            state.stateAction = action;
            state.stateAction.routineInstance = state.coroutineRunSource.runRoutine(action);
        }
        private void stopPreviousAction(){
                var msg = "stop coroutine " + state.stateAction.routineInstance.unityRoutine + " routiner finished:" + state.stateAction.routineInstance.finished;
                Debug.Log(msg);
                state.previousAction = state.stateAction;
                state.coroutineRunSource.StopCoroutine(state.previousAction.routineInstance.unityRoutine);
        }
    }
}