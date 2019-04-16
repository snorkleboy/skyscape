using System;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEditor;
namespace Objects
{

    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class StateAction : IEnumerator,ISerializationCallbackReceiver  {

        protected IEnumerator enumerator;
        public util.Routiner routineInstance;
        public string _type;
        public virtual void hydrate<T>(T source){
            throw(new NotImplementedException());
        }
        public void OnBeforeSerialize() {
            _type = this.GetType().ToString();
        }
        public void OnAfterDeserialize() {
            _type = null;
        }
        protected virtual void _Init(){
            enumerator = getEnumerator();
        }
        public virtual bool MoveNext(){
            return enumerator.MoveNext();
        }
        protected virtual IEnumerator getEnumerator(){
            throw(new NotImplementedException());
        }

        public virtual void Reset() {
            
        }
        public virtual void Destroy(){

        }
        [JsonIgnore]public object Current{get{return enumerator.Current;}}


    }


    
}