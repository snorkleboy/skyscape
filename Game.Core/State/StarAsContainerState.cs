using System;
using System.Collections.Generic;
using System.Text;
using Game.Core.Util;
using Game.Core.Entities;
using Newtonsoft.Json;
namespace Game.Core.State
{
    public class StarAsContainerState
    {
        [JsonProperty]
        public List<Reference<Planet>> planets = new List<Reference<Planet>>();
        [JsonProperty]
        public List<Reference<Fleet>> fleets = new List<Reference<Fleet>>();
    }
}
