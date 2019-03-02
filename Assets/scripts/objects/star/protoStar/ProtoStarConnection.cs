using UnityEngine;
namespace Objects.Galaxy
{
    public class ProtoStarConnection: MonoBehaviour,IAppearable
    {
        public ProtoStarConnectionState state;
        public IAppearer appearer { get; set; }
        public double strength;
        public ProtoStar[] nodes;
        public void Init(ProtoStarConnectionState state, ProtoStarConnectionRenderer renderer)
        {
            appearer = renderer;
            this.state = state;
        }
    }
}
