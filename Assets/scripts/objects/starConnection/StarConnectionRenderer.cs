using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
        [System.Serializable]
    public class StarConnectionRenderHelper : PerSceneRenderer<StarConnection>
    {
        StarNode[] nodes;
        public override Transform parent { get; set; }

        public StarConnectionRenderHelper(GameObject[] sceneToPrefab, StarNode[] nodes, StarConnection connection) : base(sceneToPrefab,nodes[0].renderHelper.parent,connection)
        {
            this.nodes = nodes;
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
        public override void destroy(){
            base.destroy();
        }
    }
}
