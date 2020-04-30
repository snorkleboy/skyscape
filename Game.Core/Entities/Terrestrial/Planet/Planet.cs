using Game.Core.Entities;
using Game.Core.State;
namespace Game.Core.Entities
{
    [System.Serializable]
    public partial class Planet : BaseGameObject<PlanetState>
    {
        //public override IAppearer appearer { get; set; }
        public PlanetState debugState { get { return this.state; } }
        public void Init(PlanetState state)
        {
            this.state = state;
        }

    }
}
