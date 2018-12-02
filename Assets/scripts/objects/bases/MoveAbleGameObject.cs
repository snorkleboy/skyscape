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
    public abstract class MoveAbleGameObject:MonoBehaviour,IUIable,IRenderable,IMoveable
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
        public virtual IRenderer renderHelper{get;set;} 
        public abstract void render(int scene);
        public abstract IMover mover{get;}
        public Sprite icon;
        public string title;
        public abstract iconInfo getIconableInfo();

    }

[CustomEditor(typeof(MoveAbleGameObject),true)]
public class MoveAbleGameObjectDrawer : Editor 
{
    public override void OnInspectorGUI()
    {
        MoveAbleGameObject myTarget = (MoveAbleGameObject)target;

        EditorGUILayout.LabelField("stateAction", myTarget.stateAction !=null ? myTarget.stateAction.ToString() : "no state");
        EditorGUILayout.LabelField("finished",myTarget.stateAction !=null ? myTarget.stateAction.routineInstance.finished.ToString() : "no state");
        EditorGUILayout.LabelField("previousAction",myTarget.previousAction !=null ? myTarget.previousAction.ToString() : "no prev state");
        EditorGUILayout.LabelField("finished",myTarget.previousAction !=null ? myTarget.previousAction.routineInstance.finished.ToString() : "no prev state");

        EditorGUILayout.LabelField("renderHelper", myTarget.renderHelper.ToString());
        EditorGUILayout.ObjectField("parent", myTarget.renderHelper.parent, typeof(Transform));
        EditorGUILayout.ObjectField("transform:", myTarget.renderHelper.transform, typeof(Transform), true);
        EditorGUILayout.LabelField("uid", myTarget.renderHelper.uid.ToString());

        EditorGUILayout.LabelField("getPosition", myTarget.mover.getPosition().ToString());
        EditorUtility.SetDirty(target);
    }

}
}
