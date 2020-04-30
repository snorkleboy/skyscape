using Newtonsoft.Json;
using UnityEngine;
using Game.Core.State;
namespace Game.Core.Entities
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class Weapon
    {
        public WeaponDescription weaponDescription;
        public virtual Weapon init(PositionState thisPosition, WeaponDescription weaponDescription){
           this.thisPosition = thisPosition;
           this.weaponDescription = weaponDescription;
           return this;
        }
        [JsonIgnore]public PositionState thisPosition;
        public abstract bool fire(PositionState target,State.DestructableState targetHealth);
        public virtual bool didHit(PositionState target){
            var distance = Vector3.Distance(thisPosition.position,target.position);
            return _didHit(distance);
        }
        public virtual bool didHit(PositionState target,out float distance){
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

}