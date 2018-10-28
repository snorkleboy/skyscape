using UnityEngine;
using System;
namespace Objects
{
    public interface IRenderer
    {
        bool render(int scene);
        Transform transform { get; }
        Transform parent { set; }
        void disable();
        void enable();
        void destroy();
        Guid uid { get; }
    }
    public interface IRenderable
    {
        IRenderer renderHelper {get;}
        void render(int context);
    }
}
