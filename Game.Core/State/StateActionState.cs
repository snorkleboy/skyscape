using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using System;

using Newtonsoft.Json;
namespace Game.Core.State
{
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class StateActionState
    {
        public StateActionState()///MonoBehaviour runSource)
        {
            //this.coroutineRunSource = runSource;
        }
        //public MonoBehaviour coroutineRunSource;
        [JsonProperty] public StateActionState stateAction = null;
        [JsonProperty] public StateActionState previousAction = null;
        //public virtual void setStateAction(StateAction action) { throw new NotImplementedException(); }
        //public virtual void run()
        //{
        //    stateAction.routineInstance = coroutineRunSource.runRoutine(stateAction);
        //}
    }

}
