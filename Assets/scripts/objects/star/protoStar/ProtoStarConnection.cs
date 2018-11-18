using UnityEngine;

namespace Objects.Galaxy
{
    public class ProtoStarConnection: IRenderable
    {
        public IRenderer renderHelper { get; set; }
        public double strength;
        public ProtoStar[] nodes;
        public void Init(double _strength, ProtoStar[] _nodes, ProtoStarConnectionRenderer renderer)
        {
            renderHelper = renderer;
            strength = _strength;
            nodes = _nodes;
        }
        public void render(int scene)
        {
            renderHelper.render(scene);
        }
    }
}
