using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    class AddPlanets: CreatorWare
    {
        public PlanetFactory planetFactory;
        public override Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> starNodes)
        {
            foreach (var starList in starNodes.Values)
            {
                foreach (var starNode in starList)
                {
                    Planet[] planets = new Planet[Random.Range(0, 10)];
                    for (int i = 0; i < planets.Length; i++)
                    {
                        planets[i] = planetFactory.newPlanet(starNode.transform);
                    }
                    starNode.planets = planets;
                }
            }
            return starNodes;
        }
    }
}
