using UnityEngine;
namespace Objects
{
    public interface IIded{
        long getId();
    }
    public class UniqueIdMaker
    {
        ObjectTable table;
        public UniqueIdMaker(long startcount, ObjectTable table){
            count = startcount;
            this.table = table;
        }
        public long count = 0;
        public long newId(object obj){
            var num = count++;
            table.set(num,obj);
            return num;
        }
        public long insertObject(object obj, long id){
            if(table.get(id) != null){
                Debug.LogError("insertObject is attempting to overwriting object in id table, id="+id);
            }
            table.set(id,obj);
            return id;
        }
    }
}