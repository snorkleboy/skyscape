using UnityEngine;

namespace Objects.Galaxy
{
    public class StarConnection: IRenderable
    {
        public IRenderer renderHelper { get; set; }
        public double strength;
        public StarNode[] nodes;

        public StarConnection(double _strength, StarNode[] _nodes, StarConnectionRenderHelper renderer)
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
