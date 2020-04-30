using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Core.State
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class DestructableState
    {
        public int hp = 100;
        public int resistance = 5;
        [JsonIgnore] public System.Action onDestroy;
        public bool changeHp(int delta)
        {
            hp = hp + delta;
            var destoyed = hp <= 0;
            if (destoyed)
            {
                onDestroy();
            }
            return destoyed;
        }
    }
   
}
