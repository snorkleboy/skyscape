using UnityEngine;

namespace Objects.Galaxy
{
    public class StarConnectionModel{
        public StarConnectionModel(StarConnection connection){
            starIds = new long[connection.nodes.Length];
            for(var i=0;i<starIds.Length;i++)
            {
                starIds[i] = connection.nodes[i].id;
            }
        }
        public long[] starIds;
    }
    public class StarConnection: MonoBehaviour,IRenderable, ISaveAble<StarConnectionModel>
    {
        public StarConnectionModel model{get{return new StarConnectionModel(this);}}
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
            // this.transform.position = nodes[0].transform.position;
        }
    }


}
