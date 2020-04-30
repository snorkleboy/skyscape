using Game.Core.Entities.Interfaces;
using Game.Core.State;
using Newtonsoft.Json;
namespace Game.Core.Entities
{
    public partial class Pop : IHasStateObject
    {
        public IHasID stateObject { get { return state; } set { state = (PopState)value; } }

        public long getId() { return state.id; }
        public PopState state { get; set; }

        // public Pop(Sprite sprite,PopModel model)
        // {
        //     popSprite = sprite;
        //     name = model.name;
        //     money = model.money;
        // }
        public Pop(PopState state)
        {
            this.state = state;
            // popSprite = sprite;
            // name = PopNames.names[UnityEngine.Random.Range(0,PopNames.names.Length-1)];
            // money = UnityEngine.Random.Range(0,100);
        }
    }

}
