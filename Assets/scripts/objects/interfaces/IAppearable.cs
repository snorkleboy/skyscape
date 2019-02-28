using UnityEngine;
using System;
using Objects.Galaxy;
using Objects.Galaxy.State;
namespace Objects
{
    public interface IStatefulAttribute<State>{
        State state{get;}
    }
    public interface IAppearer:IStatefulAttribute<AppearableState>
    {
        void setAppearTransform(Transform parent);
        bool appear(int scene);
        void destroy();
    }
    public interface IAppearable
    {
        IAppearer appearer {get;}
    }
}
