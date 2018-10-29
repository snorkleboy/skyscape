using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy.Holdable;
namespace Objects.Galaxy
{
    public class PlanetFactory: MonoBehaviour
    {
        public GameObject baseStarFab;
        public TileFactory tileFactory;

        public Planet newPlanet(Transform holder)
        {
            var rep = new PlanetRenderer(baseStarFab);
            rep.parent = holder;
            Planet planet = new Planet(rep);
            planet.tileManager = tileFactory.makeTileManager();
            return planet;
        }

    }

}
