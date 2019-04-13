using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
using Objects;
using Objects.Conceptuals;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
    public class FactoryProvider{
        public StarFactory starFactory;
        public StarConnectionFactory starConnectionFactory;
        public TileFactory tileFactory;
        public PlanetFactory planetFactory;
        public Dictionary<Faction,FleetFactory> fleetFactory;


    }
}



