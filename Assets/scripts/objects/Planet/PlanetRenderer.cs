using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]
    public class PlanetRenderer : SingleSceneRenderer<Planet>
    {
        public PlanetRenderer(GameObject prefab) : base(prefab)
        {
        }
        public override void applyScript(GameObject go, Planet script)
        {

            go.GetComponent<PlanetStub>().planet = script;
        }
    }
}
