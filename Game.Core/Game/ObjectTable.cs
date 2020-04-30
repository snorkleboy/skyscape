using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Game.Core.Entities.Interfaces;
namespace Game.Core.Entities
{
        public static class ObjectTableExtension
        {
            public static Dictionary<long, object> toStateTable(this Dictionary<long, IHasStateObject> objects)
            {
                Dictionary<long, object> states = new Dictionary<long, object>();
                foreach (var keyVal in objects)
                {
                    states[keyVal.Key] = keyVal.Value.stateObject;
                }
                return states;
            }
        }
        public static class ObjectTable
        {

            public static void init(Dictionary<long, IHasStateObject> objects)
            {
                ObjectTable.objects = objects;
            }

            public static Dictionary<long, IHasStateObject> objects = new Dictionary<long, IHasStateObject>();
            public static object get(long id)
            {
                IHasStateObject test = null;
                if (!objects.TryGetValue(id, out test))
                {
                    // Log.warnLog(this,"requested id returned null",id);
                }
                return test;
            }
            public static bool remove(long id)
            {
                return objects.Remove(id);
            }
            public static void set(long id, IHasStateObject obj)
            {
                IHasStateObject test = null;
                if (objects.TryGetValue(id, out test))
                {
                    //Log.errorLog(this, "setting id when id already exists", id, obj, test);
                }
                objects[id] = obj;
            }
        }
}
