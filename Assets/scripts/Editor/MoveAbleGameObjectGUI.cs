// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Objects.Galaxy;
// using UI;
// using Loaders;
// using util;
// using UnityEditor;
// using Objects;
// [CustomEditor(typeof(MoveAbleGameObject),true)]
// public class MoveAbleGameObjectDrawer : Editor 
// {
//     public override void OnInspectorGUI()
//     {
//         MoveAbleGameObject myTarget = (MoveAbleGameObject)target;

//         EditorGUILayout.LabelField("stateAction", myTarget.stateAction !=null ? myTarget.stateAction.ToString() : "no state");
//         EditorGUILayout.LabelField("finished",myTarget.stateAction !=null ? myTarget.stateAction.routineInstance.finished.ToString() : "no state");
//         EditorGUILayout.LabelField("previousAction",myTarget.previousAction !=null ? myTarget.previousAction.ToString() : "no prev state");
//         EditorGUILayout.LabelField("finished",myTarget.previousAction !=null ? myTarget.previousAction.routineInstance.finished.ToString() : "no prev state");

//         EditorGUILayout.LabelField("renderHelper", myTarget.appearer.ToString());
//         EditorGUILayout.ObjectField("parent", myTarget.appearer.appearTransform, typeof(Transform));
//         EditorGUILayout.ObjectField("transform:", myTarget.appearer.activeGO.transform, typeof(Transform), true);

//         EditorGUILayout.LabelField("getPosition", myTarget.mover.getPosition().ToString());
//         EditorUtility.SetDirty(target);
//     }

// }