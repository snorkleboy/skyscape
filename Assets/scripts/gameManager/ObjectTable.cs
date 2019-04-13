using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using util;
using System.Linq;
using Newtonsoft.Json;
namespace Objects
{
    public static class ReferenceExtension{
        public static List<T> getAllReferenced<T>(this IEnumerable<Reference<T>> list) where T: class,IIded{
            var returnList = new List<T>();
            foreach(var thing in list){
                returnList.Add(thing.value);
            }

            return returnList;
        }
        public static List<Reference<T>> referenceAll<T>(this IEnumerable<T> list) where T: class,IIded{
            var returnList = new List<Reference<T>>();
            foreach(var thing in list){
                returnList.Add(new Reference<T>(thing));
            }
            return returnList;
        }
        public static T dereference<T>(this long id){
            var obj = GameManager.instance.objectTable.get(id);
            if (obj == null){
                Debug.LogError("derefernce returned null for id:"+id);
            }
            var Tthing = (T)obj;
            if(Tthing == null){
                Debug.LogError("derefernce failed to cast to type:"+typeof(T) + " for id:"+id);
            }
            return Tthing;
        }
    }
    [System.Serializable]

    public class Reference<T>:IEquatable<Reference<T>> where T : class,IIded{
        public static implicit operator Reference<T>(T thing)
        {
            if(thing != null){
                return new Reference<T>(thing);
            }else{
                return null;
            }
        }
        public static implicit operator T(Reference<T> thing)
        {
            return thing.value;
        }
        public Reference(long id, bool instaLoad = false){
            this.id = id;
            if (instaLoad){
                setValue();
            }
        }
        public Reference(T obj){
            this.id = obj.getId();
            _value = obj;
        }
        public Reference(){}

        [JsonProperty]public long id;
        public long getId(){
            return id;
        }
        public bool checkExists(){
            if (_value != null){
                return true;
            }
            trySetValue();
            if (_value != null){
                return true;
            }
            return false;
        }
        private T _value;
        [JsonIgnore]public T value{
            get{
                if(_value == null){
                    setValue();
                }
                return _value;
            }
        }
        private T setValue(){
            var thing = GameManager.instance.objectTable.get(id);

            if (thing == null){
                Log.errorLog(this,"reference returned null",id);
            }
            _value = thing as T;
            if (_value == null){
                Log.errorLog(this,"reference cant be cast to specified type",typeof(T), thing,id );
            }
            return _value;
        }
        public bool Equals(Reference<T> other)
        {
            if (other == null)
            {
                return false;
            }
            return other.id == this.id;
        }
        private T trySetValue(){
            var thing = GameManager.instance.objectTable.get(id);
            if (thing != null){
                _value = thing as T;
            }
            return _value;
        }
      
    }
public static class ObjectTableExtension{
    public static Dictionary<long,object> toStateTable(this Dictionary<long,hasStateObject> objects){
        Dictionary<long,object> states = new Dictionary<long,object>();
        foreach (var keyVal in objects)
        {
            states[keyVal.Key] = keyVal.Value.stateObject;
        }
        return states;
    } 
}
public class ObjectTable{

        public ObjectTable(Dictionary<long,hasStateObject> objects){
            this.objects = objects;
        }
        public ObjectTable(){
        }
        public Dictionary<long,hasStateObject> objects = new Dictionary<long, hasStateObject>();
        public object get(long id){
            hasStateObject test = null;
            if(!objects.TryGetValue(id, out test)){
                // Log.warnLog(this,"requested id returned null",id);
            }
            return test;
        }
        public void set(long id,hasStateObject obj){
            hasStateObject test = null;
            if(objects.TryGetValue(id, out test)){
                Log.errorLog(this,"setting id when id already exists",id,obj,test);
            }
            objects[id] = obj;
        }
    }
}