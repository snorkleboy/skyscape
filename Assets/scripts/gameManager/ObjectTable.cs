using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using util;


namespace Objects
{
    public class Reference<T> where T : class{
        long id;
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
                Log.errorLog(this,"reference cant be cast to specified type",typeof(T), thing,thing.GetType(),id );
            }

            return _value;
        }

        public Reference(long id, bool instaLoad = false){
            this.id = id;
            if (instaLoad){
                setSingletonValue();
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