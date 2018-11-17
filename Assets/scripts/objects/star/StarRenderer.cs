using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    [System.Serializable]

    public class StarRenderer : HolderRenderer<StarNode>
    {
        public StarRenderer(GameObject[] sceneToPrefab, Transform holder) : base(sceneToPrefab, holder)
        {
        }
        public override void applyScript(GameObject go, StarNode script)
        {

        }
        public override bool render(int scene){
            if(base.render(scene)){
                activeGO.name = scriptSingelton.name;
            }
            return true;
        }

    }
}

