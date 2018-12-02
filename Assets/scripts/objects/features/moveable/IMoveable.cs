using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public interface IMoveable
    {
        IMover mover{get;} 
    }
    public interface IMover{
        StateAction setTarget(Vector3 target, float stopDistence = .5f);
        Vector3 getPosition();
    }
}