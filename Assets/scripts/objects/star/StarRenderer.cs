using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    public class StarRenderer : HolderRenderer<StarNode>
    {
        public StarRenderer(GameObject[] sceneToPrefab, Transform holder) : base(sceneToPrefab, holder)
        {
        }
        public override void applyScript(GameObject go, StarNode script)
        {
           var stub = go.GetComponent<StarStub>();
           bool fullStar = stub != null;
           if (fullStar){
               stub.set(script);
           }
        }

    }
}

