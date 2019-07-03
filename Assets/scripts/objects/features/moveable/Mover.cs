using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects.Galaxy.State;

namespace Objects
{
    public interface IPositionable
    {
        AppearablePositionState positionState { get; }
    }
    public interface IMoveable: IPositionable,IActionable
    {
    }


}