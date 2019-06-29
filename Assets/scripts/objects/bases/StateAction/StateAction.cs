using System;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEditor;
namespace Objects
{


    public abstract class StateAction : IEnumerator  {

        protected IEnumerator enumerator;
        public util.Routiner routineInstance;

        protected virtual void _Init(){
            enumerator = getEnumerator();
        }
        public virtual bool MoveNext(){
            return enumerator.MoveNext();
        }
        public virtual StateAction hydrate<T>(T source){
            throw(new NotImplementedException());
        }
        protected abstract IEnumerator getEnumerator();
        public virtual void Reset() {
        }
        public virtual void Destroy(){
        }
        [JsonIgnore]public object Current{get{return enumerator.Current;}}
    }
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class SaveableStateAction:StateAction,ISerializationCallbackReceiver{
        public string _type;
        public override abstract StateAction hydrate<T>(T source);
        protected T tryCoerce<I,T>(I source)where T:class{
            var thing = source as T;
            if (thing == null){
                Debug.LogError("couldnt coerce " + typeof(I) + " to " + typeof(T) + " " + source);
            }
            return thing;
        }
        public void OnBeforeSerialize() {
            _type = this.GetType().ToString();
        }
        public void OnAfterDeserialize() {
            _type = null;
        }
    }


    
}