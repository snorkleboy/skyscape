using System;
using System.Collections.Generic;
using System.Text;
using Game.Core.Entities.Interfaces;
using Game.Core.Entities;
namespace Game.Application
{
    public class UniqueIdMaker
    {
        public UniqueIdMaker(long startcount)
        {
            count = startcount;
        }
        public long count = 0;
        public long newId(IHasStateObject obj)
        {
            var num = count++;
            ObjectTable.set(num, obj);
            return num;
        }
        public long insertObject(IHasStateObject obj, long id)
        {
            if (ObjectTable.get(id) != null)
            {
                //Debug.LogError("insertObject is attempting to overwrit object in id table, id = " + id);
            }
            ObjectTable.set(id, obj);
            return id;
        }
        public bool removeObject(long id)
        {
            return ObjectTable.remove(id);
        }
    }
}
