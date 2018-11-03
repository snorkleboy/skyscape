using UnityEngine;

namespace UI
{
    public abstract class Stub<T> : MonoBehaviour where T: IUIable
    {
        T stub;
        public void setStub(T thing){
            stub = thing;
        }

    }
}