using System;
using System.Collections.Generic;
using Game.Core.Entities.Interfaces;
using Newtonsoft.Json;
using Game.Core.Entities;
namespace Game.Core.Util
{

    [Serializable]
    public class Reference<T> : IEquatable<Reference<T>> where T : class, IHasID
    {
        public static implicit operator Reference<T>(T thing)
        {
            if (thing != null)
            {
                return new Reference<T>(thing);
            }
            else
            {
                return null;
            }
        }
        public static implicit operator T(Reference<T> thing)
        {
            return thing.value;
        }
        public Reference(long id, bool instaLoad = false)
        {
            this.id = id;
            if (instaLoad)
            {
                setValue();
            }
        }
        public Reference(T obj)
        {
            this.id = obj.getId();
            _value = obj;
        }
        public Reference() { }

        [JsonProperty] public long id;
        public long getId()
        {
            return id;
        }
        public bool checkExists()
        {
            if (_value != null)
            {
                return true;
            }
            trySetValue();
            if (_value != null)
            {
                return true;
            }
            return false;
        }
        private T _value;
        [JsonIgnore]
        public T value
        {
            get
            {
                if (_value == null)
                {
                    setValue();
                }
                return _value;
            }
        }

        public bool Equals(Reference<T> other)
        {
            if (other == null)
            {
                return false;
            }
            return other.id == this.id;
        }
        private T setValue()
        {
            var thing = "";//GameManager.instance.objectTable.get(id);

            if (thing == null)
            {
                //Log.errorLog(this, "reference returned null", id);
            }
            _value = thing as T;
            if (_value == null)
            {
                //Log.errorLog(this, "reference cant be cast to specified type", typeof(T), thing, id);
            }
            return _value;
        }
        private T trySetValue()
        {
            var thing = "";//GameManager.instance.objectTable.get(id);
            if (thing != null)
            {
                _value = thing as T;
            }
            return _value;
        }

    }

    public static class ReferenceExtension
    {
        public static List<T> getAllReferenced<T>(this IEnumerable<Reference<T>> list) where T : class, IHasID
        {
            var returnList = new List<T>();
            foreach (var thing in list)
            {
                returnList.Add(thing.value);
            }

            return returnList;
        }
        public static List<Reference<T>> referenceAll<T>(this IEnumerable<T> list) where T : class, IHasID
        {
            var returnList = new List<Reference<T>>();
            foreach (var thing in list)
            {
                returnList.Add(new Reference<T>(thing));
            }
            return returnList;
        }


        public static T dereference<T>(this long id)
        {
            var obj = ObjectTable.get(id);
            if (obj == null)
            {
                //Debug.LogError("derefernce returned null for id:" + id);
            }
            var Tthing = (T)obj;
            if (Tthing == null)
            {
                //Debug.LogError("derefernce failed to cast to type:" + typeof(T) + " for id:" + id);
            }
            return Tthing;
        }
    }
}
