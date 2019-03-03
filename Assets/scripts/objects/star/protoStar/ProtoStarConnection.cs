using UnityEngine;
namespace Objects.Galaxy
{
    public class ProtoStarConnection: IAppearable
    {
        public ProtoStarConnectionState state;
        public IAppearer appearer { get; set; }
        public double strength;
        public void Init(ProtoStarConnectionState state, ProtoStarConnectionRenderer renderer)
        {
            appearer = renderer;
            this.state = state;
        }
    }
}
