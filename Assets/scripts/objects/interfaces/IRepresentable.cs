using UnityEngine;

namespace Objects
{
    public interface IRepresentable
    {
        Transform transform { get; set; }
        void enter();
        void exit();
        void destroy();
    }
}