using UnityEngine;
using System;
namespace Objects
{
    public interface IAppearer
    {
        void setAppearPosition(Vector3 position, int scene);
        Vector3 getAppearPosition(int scene);

        GameObject activeGO { get; }
        Transform appearTransform { set;get; }
        bool isActive{get;}
        bool appear(int scene);
        void destroy();
    }
    public interface IAppearable
    {
        IAppearer appearer {get;}
        Transform transform{get;}
        void appear(int context);
    }
}
