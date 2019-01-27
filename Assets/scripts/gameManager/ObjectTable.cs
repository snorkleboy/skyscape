using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using util;
using System.Linq;

namespace Objects
{
    public static class ReferenceExtension{
        public static List<T> getAllReferenced<T>(this IEnumerable<Reference<T>> list) where T: class{
            var returnList = new List<T>();
            foreach(var thing in list){
                returnList.Add(thing.value);
            }

            return returnList;
        }
    }
    [System.Serializable]
    public class Reference<T> where T : class{

        [SerializeField]long id;
        public long getId(){
            return id;
        }
        public bool checkExists(){
            if (_value != null){
                return true;
            }
            trySetSingletonValue();
            if (_value != null){
                return true;
            }
            return false;
        }
        private T _value;
        public T value{
            get{
                if(_value == null){
                    setSingletonValue();
                }
                return _value;
            }
        }
        private T setSingletonValue(){
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
        private T trySetSingletonValue(){
            var thing = GameManager.instance.objectTable.get(id);
            if (thing != null){
                _value = thing as T;
            }
            return _value;
        }
        public Reference(long id, bool instaLoad = false){
            this.id = id;
            if (instaLoad){
                setSingletonValue();
            }
        }
        public Reference(IIded obj){
            this.id = obj.getId();
            _value = (T)obj;
            if(_value == null){
                Debug.LogError(this + ": coulnt make " + obj + " into " + typeof(T));
            }
        }

    }
public class ObjectTable{
        public ObjectTable(Dictionary<long,object> objects){
            this.objects = objects;
        }
        public ObjectTable(){
        }
        private Dictionary<long,object> objects = new Dictionary<long, object>();
        public object get(long id){
            object test = null;
            if(!objects.TryGetValue(id, out test)){
                Log.warnLog(this,"requested id returned null",id);
            }
            return test;
        }
        public void set(long id,object obj){
            object test = null;
            if(objects.TryGetValue(id, out test)){
                Log.errorLog(this,"setting id when id already exists",id,obj,test);
            }
            objects[id] = obj;
        }
    }
}