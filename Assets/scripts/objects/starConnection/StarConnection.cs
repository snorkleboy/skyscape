using UnityEngine;
using UnityEditor;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    public class StarConnectionModel{
        public StarConnectionModel(){}

        public StarConnectionModel(StarConnection connection){
            starIds = new long[connection.state.nodes.Length];
            for(var i=0;i<starIds.Length;i++)
            {
                starIds[i] = connection.state.nodes[i].getId();
            }
        }
        public long[] starIds;
    }
    public class StarConnectionState
    {        
        public double strength;
        public Reference<StarNode>[] nodes;
        public AppearableState appearableState;
    }
    public class StarConnection: MonoBehaviour,IAppearable, ISaveAble<StarConnectionModel>
    {
        public StarConnectionModel model{get{return new StarConnectionModel(this);}}
        public IAppearer appearer { get; set; }
        public StarConnectionState state;

        public void Init(StarConnectionState state, StarConnectionAppearer renderer)
        {
            appearer = renderer;
            this.state = state;
        }

    }
    [CustomEditor(typeof(StarConnection),true)]
    public class StarConnectionEditor : Editor 
    {
        public override void OnInspectorGUI() {
            StarConnection myTarget = (StarConnection)target;
            EditorGUILayout.LabelField("stars: ");
            EditorGUILayout.BeginVertical ();
            foreach (var star in myTarget.state.nodes.getAllReferenced())
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width((float)(EditorGUIUtility.currentViewWidth*.8)));
                    EditorGUILayout.LabelField("star id", star.state.id.ToString());
                    EditorGUILayout.ObjectField("value",star,typeof(StarNode),true);
                EditorGUILayout.EndHorizontal();
            } 
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(target);
        }
    }

}
