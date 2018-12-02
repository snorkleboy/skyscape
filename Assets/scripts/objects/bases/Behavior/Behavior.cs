using UnityEngine;
using System.Collections;
namespace Objects
{

    [System.Serializable]
    public abstract class StateAction : IEnumerator {
        protected IEnumerator enumerator;
        protected virtual void Init(){
            enumerator = getEnumerator();
        }
        public virtual bool MoveNext(){
            if(enumerator == null){
                enumerator = getEnumerator();
            }
            var ran = enumerator.MoveNext();
            return ran;
        }
        protected abstract IEnumerator getEnumerator();

        public void Reset() {
            
        }
        public virtual void Destroy(){

        }
        public object Current{get{return enumerator.Current;}}

    }
}