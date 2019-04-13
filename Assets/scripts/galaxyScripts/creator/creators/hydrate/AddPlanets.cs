using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;
using Objects;
namespace GalaxyCreators
{
    class AddPlanets: StarMaker
    {
        public PlanetFactory planetFactory;
        public override Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> starNodes)
        {
            foreach (var starList in starNodes.Values)
            {
                foreach (var starNode in starList)
                {
                    var planets = new Planet[Random.Range(0, 10)];
                    var multiplier = 1.0;
                    for (int i = 0; i < planets.Length; i++)
                    {
                        multiplier = Random.Range(.8f,1.2f) * multiplier;
                        multiplier++;
                        var position = Vector3.right*(int)(30*multiplier);
                        var planet =  planetFactory.makePlanet(starNode,position);
                        if (i>2){
                            multiplier++;
                        }
                        if (i>4){
                            multiplier++;
                        }
                        planets[i] = planet;
                        
                    }
                
                    starNode.enterable.setPlanets(planets);
                }
            }
            return starNodes;
        }
    }
}
