using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Galaxy.State;

namespace Objects
{
    public interface IMoveable
    {
        IMover mover{get;} 
    }
    public interface IMover{
        void moveTo(Vector3 target, float stopDistence = .5f);
        AppearableState appearableState{get;}
        StateActionState stateActionState{get;}
    }


}