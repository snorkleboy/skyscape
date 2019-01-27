using UnityEngine;
using UnityEditor;
namespace Objects.Galaxy
{
    public class StarConnectionModel{
        public StarConnectionModel(){}

        public StarConnectionModel(StarConnection connection){
            starIds = new long[connection.nodes.Length];
            for(var i=0;i<starIds.Length;i++)
            {
                starIds[i] = connection.nodes[i].getId();
            }
        }
        public long[] starIds;
    }
    public class StarConnection: MonoBehaviour,IAppearable, ISaveAble<StarConnectionModel>
    {
        public StarConnectionModel model{get{return new StarConnectionModel(this);}}
        public IAppearer appearer { get; set; }
        public double strength;
        public Reference<StarNode>[] nodes;

        public void Init(double _strength, Reference<StarNode>[] _nodes, StarConnectionAppearer renderer)
        {
            appearer = renderer;
            // renderer.scriptSingelton = this;
            strength = _strength;
            nodes = _nodes;
        }
        public void appear(int scene)
        {
            appearer.appear(scene);
        }

    }
    [CustomEditor(typeof(StarConnection),true)]
    public class StarConnectionEditor : Editor 
    {
        public override void OnInspectorGUI() {
            StarConnection myTarget = (StarConnection)target;
            EditorGUILayout.LabelField("stars: ");
            EditorGUILayout.BeginVertical ();
            foreach (var star in myTarget.nodes.getAllReferenced())
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width((float)(EditorGUIUtility.currentViewWidth*.8)));
                    EditorGUILayout.LabelField("star id", star.id.ToString());
                    EditorGUILayout.ObjectField("value",star,typeof(StarNode),true);
                EditorGUILayout.EndHorizontal();
            } 
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(target);
        }
    }

}
