using System;
namespace Objects
{

    [System.Serializable]
    public class FactoryStamp
    {
        public FactoryStamp(Enum stamp = null){
            this.stamp = stamp;
        }
        public Enum stamp;

    }
}