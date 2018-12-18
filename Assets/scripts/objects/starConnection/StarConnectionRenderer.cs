using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
        [System.Serializable]
    public class StarConnectionAppearer : MultiSceneAppearer
    {
        StarNode[] nodes;
        public override Transform attachementPoint { get; set; }
    
        public StarConnectionAppearer(sceneAppearInfo[] sceneToPrefab, StarNode[] nodes) : base(sceneToPrefab,nodes[0].appearer.attachementPoint)
        {
            this.nodes = nodes;
        }
        public override bool appear(int scene)
        {
            if (base.appear(scene))
            {
                if (scene == (int)util.Enums.sceneNames.GalaxyView || scene == (int)util.Enums.sceneNames.mainMenu)
                {
                    if(nodes[0].appearer.activeGO != null && nodes[1].appearer.activeGO != null){
                        var line = activeGO.GetComponent<DrawLineBetweenPoints>();
                        line.setTarget(nodes[0].appearer.activeGO, 0);
                        line.setTarget(nodes[1].appearer.activeGO, 1);
                        line.draw();
                    }
                }else if (scene == 3){
                    var connectionControl = activeGO.GetComponent<starNodeConnectionController>();
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
