using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Game.Core.Entities.Interfaces;
using Game.Core.State;
namespace Game.Core.Entities
{

    public abstract class BaseGameObject<StateModel> :  IHasStateObject,  IMoveable, IActionable where StateModel : IHasID, IActionable, IMoveable
    {

        [JsonProperty]
        public StateModel state { get; set; }

        public long getId()
        {
            if (state == null)
            {
                //Debug.LogError("calling null state");
            }
            return state.getId();
        }
        public IHasID stateObject { get { return state; } set { state = (StateModel)value; } }


        public PositionState positionState { get { return state.positionState; } }
        public StateActionState stateActionState { get { return state.stateActionState; } }
        //InputReceiver receiver;

        //public abstract IconInfo getIconableInfo();
        //public virtual IAppearer appearer { get; set; }
    }

}
