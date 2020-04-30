using System;
using System.Collections.Generic;
using Game.Core.Entities.Interfaces;
using Game.Core.Util;
using Game.Core.Entities;
namespace Game.Core.State
{
    [System.Serializable]
    public class FactionState : IHasID {
        public Dictionary<long, Reference<Planet>> ownedPlanets = new Dictionary<long, Reference<Planet>>();
        public Dictionary<long, Reference<Fleet>> fleets = new Dictionary<long, Reference<Fleet>>();
        public long id;
        public long getId()
        {
            return id;
        }
		public string factionName;
		public int money;
	}
}

