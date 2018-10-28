using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class PlanetFactory: MonoBehaviour
    {
        [SerializeField] public GameObject baseStarFab;

        public Planet newPlanet(Transform holder)
        {
            var rep = new PlanetRenderer(baseStarFab);
            rep.parent = holder;
            Planet planet = new Planet(rep);
            return planet;
        }

    }

}
