using UnityEngine;
using System;
using Objects.Galaxy;
using Objects.Galaxy.State;
namespace Objects
{

    public interface IAppearer
    {
        AppearableState state{get;}
        bool appear(int scene);
        void destroy();
    }
    public interface IAppearable
    {
        IAppearer appearer {get;}
    }
}
