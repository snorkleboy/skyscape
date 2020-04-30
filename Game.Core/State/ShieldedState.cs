using System;
using Newtonsoft.Json;

namespace Game.Core.State
{

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class ShieldedState
    {
        public int shieldhp = 100;
        public int rechargeRate = 20;
        public int physicalhp = 100;
    }
}
