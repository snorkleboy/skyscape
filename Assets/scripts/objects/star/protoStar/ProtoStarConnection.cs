using UnityEngine;

namespace Objects.Galaxy
{
    public class ProtoStarConnection: IAppearable
    {
        public IAppearer appearer { get; set; }
        public Transform transform{get;}
        public double strength;
        public ProtoStar[] nodes;
        public void Init(double _strength, ProtoStar[] _nodes, ProtoStarConnectionRenderer renderer)
        {
            appearer = renderer;
            strength = _strength;
            nodes = _nodes;
        }
        public void appear(int scene)
        {
            appearer.appear(scene);
        }
    }
}
