using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Newtonsoft.Json;
namespace Objects.Galaxy.ship
{
    public class EngageShip:StateAction{
        [JsonProperty]public Ship target;
        Ship thisShip;
        System.Func<object> onDestroyTarget;
        public EngageShip Init(Ship thisShip,Ship target,System.Func<object> onDestroyTarget){
            this.thisShip = thisShip;
            this.target = target;
            this.onDestroyTarget = onDestroyTarget;
            base._Init();
            return this;
        }
        protected override IEnumerator getEnumerator(){
            yield return util.Routiner.Any(
                new FollowReference().Init(thisShip.state.positionState,target.state.positionState,15f,15),
                new ShootShip().Init(thisShip,target)
            );
            if(onDestroyTarget != null){
                yield return onDestroyTarget();
            }
        }
    }
}