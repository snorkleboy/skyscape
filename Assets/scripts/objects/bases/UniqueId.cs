namespace Objects
{
    public class UniqueId
    {
        ObjectTable table;
        public UniqueId(long startcount, ObjectTable table){
            count = startcount;
            this.table = table;
        }
        public long count = 0;
        public long newId(object obj){
            var num = count++;
            table.set(num,obj);
            return num;
        }
    }
}