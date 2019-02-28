using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using UnityEditor;
namespace Objects.Galaxy
{
    [CustomEditor(typeof(Connectable),true)]
    public class ConnectableEditor : Editor 
    {
        public override void OnInspectorGUI() {
            Connectable myTarget = (Connectable)target;
            EditorGUILayout.LabelField("connections: ");
            EditorGUILayout.BeginVertical();
            foreach (var connection in myTarget.connections)
            {
                    foreach (var starRef in connection.nodes)
                    {
                        EditorGUILayout.BeginHorizontal(GUILayout.Width((float)(EditorGUIUtility.currentViewWidth*.8)));
                            EditorGUILayout.LabelField("star id", starRef.getId().ToString());
                            EditorGUILayout.ObjectField("value",starRef.value,typeof(StarNode));
                        EditorGUILayout.EndHorizontal();
                    } 
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(target);

        }
    }

    [CustomEditor(typeof(StarAsContainerState),true)]
    public class PlanetableEditor : Editor 
    {
        public override void OnInspectorGUI() {
            StarAsContainerState myTarget = (StarAsContainerState)target;
            EditorGUILayout.LabelField("planets: ");
            EditorGUILayout.BeginVertical ();
            foreach (var planet in myTarget.planets.getAllReferenced())
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width((float)(EditorGUIUtility.currentViewWidth*.8)));
                    EditorGUILayout.LabelField("planet id", planet.id.ToString());
                    EditorGUILayout.ObjectField("value",planet,typeof(Planet),true);
                EditorGUILayout.EndHorizontal();
            } 
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(target);
        }
    }

}