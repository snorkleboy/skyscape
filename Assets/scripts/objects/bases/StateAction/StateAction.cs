using UnityEngine;
using System.Collections;
namespace Objects
{
    // public abstract class StateActionModel{
    //     public StateActionModel(){}
    //     public string constructorName;

    //     public abstract StateAction hydrate<T>(T stateSource);
    // }

    [System.Serializable]
    public abstract class StateAction : IEnumerator  {/*ISaveAble<StateActionModel> */
        // public virtual StateActionModel model{get;}
        protected IEnumerator enumerator;
        public util.Routiner routineInstance;
        protected virtual void _Init(){
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

        public virtual void Reset() {
            
        }
        public virtual void Destroy(){

        }
        public object Current{get{return enumerator.Current;}}

    }
}