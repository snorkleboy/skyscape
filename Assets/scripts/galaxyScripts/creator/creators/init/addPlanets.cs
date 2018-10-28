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
                    var multiplier = 1.0;
                    for (int i = 0; i < planets.Length; i++)
                    {
                        var planet =  planetFactory.newPlanet(starNode.transform);
                        if (i>2){
                            multiplier++;
                        }
                        if (i>4){
                            multiplier++;
                        }
                        multiplier = Random.Range(.8f,1.2f) * multiplier;
                        planet.position =Vector3.right*(int)(30*multiplier);
                        planets[i] = planet;
                        multiplier++;
                    }
                    starNode.planets = planets;
                }
            }
            return starNodes;
        }
    }
}
