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
        StarConnection conn;
        public StarConnectionAppearer(sceneAppearInfo[] sceneToPrefab, StarConnection conn,StarNode[] nodes) : base(sceneToPrefab,nodes[0].appearer.attachementPoint)
        {
            this.nodes = nodes;
            this.conn = conn;
        }
        private StarNode setPositionForStarView(){
            var selectedStar = GameManager.instance.selectedStar;
            StarNode starAt;
            StarNode OtherStar;
            if(nodes[0] == selectedStar){
                starAt = nodes[0];
                OtherStar = nodes[1]; 			
            }else{
                starAt = nodes[1];
                OtherStar = nodes[0]; 			
            }
            
            Vector3 direction = OtherStar.appearer.getAppearPosition(2) - starAt.appearer.getAppearPosition(2);
            var pos = Vector3.MoveTowards(selectedStar.appearer.getAppearPosition(3),selectedStar.appearer.getAppearPosition(3)+direction.normalized*500,500);
            setAppearPosition(pos,3);// 
            Debug.Log(pos + " pos|" + direction + " direction| "  + " " + selectedStar.appearer.getAppearPosition(3) + " selectedStar.appearer.getAppearPosition(3)|" + OtherStar.appearer.getAppearPosition(2) + " OtherStar.appearer.getAppearPosition(2)|" + " " + starAt.appearer.getAppearPosition(2) + " starAt.appearer.getAppearPosition(2) |"  );
            return starAt;
        }
        private void drawLineForGalaxyView(){
            if(nodes[0].appearer.activeGO != null && nodes[1].appearer.activeGO != null){
                var line = activeGO.GetComponent<DrawLineBetweenPoints>();
                line.setTarget(nodes[0].appearer.activeGO, 0);
                line.setTarget(nodes[1].appearer.activeGO, 1);
                line.draw();
            }
        }
        public override bool appear(int scene)
        {
            StarNode starAt;
            if (scene == 3){
                starAt = setPositionForStarView();
            }
            if (base.appear(scene))
            {
                if (scene == (int)util.Enums.sceneNames.GalaxyView || scene == (int)util.Enums.sceneNames.mainMenu)
                {
                    drawLineForGalaxyView();
                }
                if(scene == 3){
                    activeGO.GetComponent<starNodeConnectionController>().set(conn);
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
