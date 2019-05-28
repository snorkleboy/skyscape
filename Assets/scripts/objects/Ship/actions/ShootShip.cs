using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Newtonsoft.Json;
namespace Objects.Galaxy.ship
{
    public class ShootShip:StateAction{
        [JsonProperty]public Ship target;
        Ship thisShip;
        System.Func<object> onDestroyTarget;
        public ShootShip Init(Ship thisShip,Ship target){
            this.thisShip = thisShip;
            this.target = target;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            bool destroyedTarget = false;
            while(!destroyedTarget){
                destroyedTarget = thisShip.state.weapons[0].fire(target.state.positionState,target.state.destructableState);
                Debug.Log("SHOOTSHIP - destroyed = " + destroyedTarget);
                yield return util.Routiner.wait(thisShip.state.weapons[0].weaponDescription.fireRate);
            }
            Debug.Log("SHIP DESTROYED");

            if(onDestroyTarget != null){
                yield return onDestroyTarget();
            }
        }
    }
}