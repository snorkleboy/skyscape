using UnityEngine;

namespace UI
{
    public class Stub : MonoBehaviour{
        protected void setUI<T>(T obj)where T : IUIable{
            var icon = GetComponentInChildren<SpaceableIcon>();
            icon.set(obj);
        }
    }

}