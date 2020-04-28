using UnityEngine;
using Newtonsoft.Json;
namespace Objects.Galaxy.weapon
{
    [System.Serializable]
    public struct WeaponDescription{
        public int damage;
        public float fireRate;
        public int accuracy;
        public int maxDistance;
    }
    [System.Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class Weapon
    {
        public WeaponDescription weaponDescription;
        public virtual Weapon init(State.AppearablePositionState thisPosition, WeaponDescription weaponDescription){
           this.thisPosition = thisPosition;
           this.weaponDescription = weaponDescription;
           return this;
        }
        [JsonIgnore]public State.AppearablePositionState thisPosition;
        public abstract bool fire(State.AppearablePositionState target,State.DestructableState targetHealth);
        public virtual bool didHit(State.AppearablePositionState target){
            var distance = Vector3.Distance(thisPosition.position,target.position);
            return _didHit(distance);
        }
        public virtual bool didHit(State.AppearablePositionState target,out float distance){
            distance = Vector3.Distance(thisPosition.position,target.position);
            return _didHit(distance);
        }
        private bool _didHit(float distance){
            bool didHit = false;
            if(distance < weaponDescription.maxDistance){
                var wouldHitChange = weaponDescription.accuracy *(weaponDescription.maxDistance / distance);
                didHit = Random.Range(0,99)<wouldHitChange;
                Debug.Log("did hit d=" + distance + " md=" + weaponDescription.maxDistance + " wouldHitChange=" + wouldHitChange);
            }
            return didHit;
        } 

    }
    public class Projectile : Weapon{
        public override bool fire(State.AppearablePositionState target,State.DestructableState targetHealt){
            int damageDone = 0;
            return false;
        }
    }
    public class SimpleLaser:Weapon{
        public override bool fire(State.AppearablePositionState target,State.DestructableState targetHealth){
            util.Line.DrawTempLine(thisPosition.position,target.position,Color.blue);
            var hit = didHit(target);
            int damageDone = hit? weaponDescription.damage : 0;
            return targetHealth.changeHp(-damageDone);
        }
    }

}