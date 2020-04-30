using Game.Core.State;
namespace Game.Core.Entities
{
    public partial class StarNode : BaseGameObject<StarNodeState>
    {

        //public StarNodeState stateForDebug;

        public void Init(StarNodeState state)
        {
            this.state = state;
            //stateForDebug = state;
            //this.appearer = renderer;
            //this.enterable = new EnterableStar(state.asContainerState);
            //this.gameObject.name = state.namedState.name;
        }
        //public override IAppearer appearer { get; set; }
        //public EnterableStar enterable;
    }

}
