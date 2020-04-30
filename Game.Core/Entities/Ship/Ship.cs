using Game.Core.State;
namespace Game.Core.Entities
{
    public class Ship : BaseGameObject<ShipState>
    {
        //public ShipState debugState;
        public virtual void Init(ShipState state)
        {
            this.state = state;
            //this.debugState = state;
            //appearer = renderer;
        }
    }

}
