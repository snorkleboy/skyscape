using Game.Core.Entities.Interfaces;
using Game.Core.Entities;
using Newtonsoft.Json;
namespace Game.Core.State
{


    [JsonObject(MemberSerialization.OptOut)]

    public class TerrestrialState : IHasID
    {
        public long id;
        public long getId() { return id; }
        public TerrestrialState() { }
        public Planet planetOn;
        public FactionOwnedState factionOwnedState;
        public NamedState named;
        //[JsonIgnore] public Sprite sprite;


    }

}