using System.Runtime.Serialization;
using Newtonsoft.Json;
using Game.Core.Entities.Interfaces;
using Game.Core.State;
namespace Game.Core.State
{
    [System.Serializable]
    [DataContract]
    public class GalaxyGameObjectState : IMoveable, IActionable, IHasID
    {

        public GalaxyGameObjectState() { }
        public GalaxyGameObjectState( long id, NamedState namedState, PositionState positionState,FactionOwnedState factionOwnedState, StateActionState actionState)
        {
            //this.icon = icon;
            this.id = id;
            //this.stamp = stamp;
            this.namedState = namedState;
            this.positionState = positionState;
            this.stateActionState = actionState;
            this.factionOwnedState = factionOwnedState;
        }
        //public Sprite icon;
        [DataMember] public long id;
        public long getId() { return id; }
        //[DataMember] public FactoryStamp stamp;
        [DataMember] public NamedState namedState;
        [DataMember] public PositionState positionState { get; set; }
        [DataMember] public StateActionState stateActionState { get; set; }
        [DataMember] public FactionOwnedState factionOwnedState;
    }

}
