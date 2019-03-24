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

    public abstract class StateActionState{
        public StateActionState(MonoBehaviour runSource){
            this.coroutineRunSource = runSource;
        }
        public MonoBehaviour coroutineRunSource;

        public Objects.StateAction stateAction = null;
        public Objects.StateAction previousAction = null;
        public abstract void setStateAction(StateAction action);

    }

    public class ControlledStateActionState :StateActionState{
        public ControlledStateActionState(MonoBehaviour runSource):base(runSource){}
        public override void setStateAction(StateAction action){
            Debug.Log("set state action (controlled)" + action);
            stateAction = action;
        }
    }
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
            stateAction.routineInstance = coroutineRunSource.runRoutine(action);
        }
    }
}