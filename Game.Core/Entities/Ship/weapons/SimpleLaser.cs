using Game.Core.State;
namespace Game.Core.Entities
{
    public class SimpleLaser:Weapon{
        public override bool fire(PositionState target,State.DestructableState targetHealth){
            //util.Line.DrawTempLine(thisPosition.position,target.position,Color.blue);
            var hit = didHit(target);
            int damageDone = hit? weaponDescription.damage : 0;
            return targetHealth.changeHp(-damageDone);
        }
    }

}