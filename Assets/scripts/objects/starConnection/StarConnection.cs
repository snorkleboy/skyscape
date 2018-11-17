using UnityEngine;

namespace Objects.Galaxy
{
    public class StarConnection: MonoBehaviour,IRenderable
    {
        public IRenderer renderHelper { get; set; }
        public double strength;
        public StarNode[] nodes;

        public void Init(double _strength, StarNode[] _nodes, StarConnectionRenderHelper renderer)
        {
            renderHelper = renderer;
            renderer.scriptSingelton = this;
            strength = _strength;
            nodes = _nodes;
        }
        public void render(int scene)
        {
            renderHelper.render(scene);
        }
    }


}
