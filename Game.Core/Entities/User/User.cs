using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core.Entities
{
    public class User
    {
        public Faction faction;
        public User(Faction faction)
        {
            this.faction = faction;
        }
    }
}
