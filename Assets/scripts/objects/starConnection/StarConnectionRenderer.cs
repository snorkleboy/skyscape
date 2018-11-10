using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
    public class StarConnectionRenderHelper : PerSceneRenderer<StarConnection>
    {
        StarNode[] nodes;
        public StarConnectionRenderHelper(GameObject[] sceneToPrefab, StarNode[] nodes) : base(sceneToPrefab)
        {
            this.nodes = nodes;
        }
        public override void applyScript(GameObject go, StarConnection script)
        {
            var stub = go.GetComponent<ConnectionStub>();
        }
        public override bool render(int scene)
        {
            if (base.render(scene))
            {
                if (scene == (int)util.Enums.sceneNames.GalaxyView || scene == (int)util.Enums.sceneNames.mainMenu)
                {
                    if ((nodes[0].transform != null && nodes[1].transform != null))
                    {
                        var line = activeGO.GetComponent<DrawLineBetweenPoints>();
                        line.setTarget(nodes[0].transform.gameObject, 0);
                        line.setTarget(nodes[1].transform.gameObject, 1);
                        line.draw();
                    }

                }else if (scene == 3){
                    var connectionControl = activeGO.GetComponent<starNodeConnectionController>();
                    connectionControl.set(scriptSingelton);
                }
                return true;
            }
            return false;
        }
    }
}
