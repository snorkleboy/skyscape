using Game.Core.State;
namespace Game.Core.Entities
{
    public class Projectile : Weapon{
        public override bool fire(PositionState target,State.DestructableState targetHealt){
            int damageDone = 0;
            return false;
        }
    }

}