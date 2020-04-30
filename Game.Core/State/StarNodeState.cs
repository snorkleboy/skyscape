using System.Runtime.Serialization;
using Newtonsoft.Json;
using Game.Core.State;
namespace Game.Core.State
{
    [System.Serializable]
    public class StarNodeState : GalaxyGameObjectState
    {
        public StarNodeState() { }
        [DataMember] public StarAsContainerState asContainerState;
        public StarNodeState(StarAsContainerState asContainerState, long id, NamedState namedState, PositionState positionState, FactionOwnedState factionOwned, StateActionState actionState) : base(id, namedState, positionState, factionOwned, actionState)
        {
            this.asContainerState = asContainerState;
        }
    }
}
