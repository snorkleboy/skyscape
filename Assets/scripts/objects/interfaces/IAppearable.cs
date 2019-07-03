using UnityEngine;
using System;
using Objects.Galaxy;
using Objects.Galaxy.State;
namespace Objects
{

    public interface IAppearer
    {
        AppearablePositionState state { get; set; }
        bool appear(int scene);
        void destroy();
    }
    public interface IAppearable
    {
        IAppearer appearer {get;}
    }
}
