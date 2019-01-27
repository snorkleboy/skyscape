using System.Collections.Generic;
using UnityEngine;
using System;

namespace Objects.Galaxy
{
        [System.Serializable]
    public class StarConnectionAppearer : MultiSceneAppearer
    {
        Reference<StarNode>[] nodes;
        public override Transform attachementPoint { get; set; }
        StarConnection conn;
        public StarConnectionAppearer(sceneAppearInfo[] sceneToPrefab, StarConnection conn,Reference<StarNode>[] nodes) : base(sceneToPrefab,nodes[0].value.appearer.attachementPoint)
        {
            this.nodes = nodes;
            this.conn = conn;
            this.preAppear = preAppearFunc;
            this.postAppear = postAppearFunc;
        }
        private StarNode setPositionForStarView(){
            var selectedStar = GameManager.instance.selectedStar;
            StarNode starAt;
            StarNode OtherStar;
            if(nodes[0].value == selectedStar){
                starAt = nodes[0].value;
                OtherStar = nodes[1].value; 			
            }else{
                starAt = nodes[1].value;
                OtherStar = nodes[0].value; 			
            }
            
            Vector3 direction = OtherStar.appearer.getAppearPosition(2) - starAt.appearer.getAppearPosition(2);
            var pos = Vector3.MoveTowards(selectedStar.appearer.getAppearPosition(3),selectedStar.appearer.getAppearPosition(3)+direction.normalized*500,500);
            setAppearPosition(pos,3);// 
            Debug.Log(pos + " pos|" + direction + " direction| "  + " " + selectedStar.appearer.getAppearPosition(3) + " selectedStar.appearer.getAppearPosition(3)|" + OtherStar.appearer.getAppearPosition(2) + " OtherStar.appearer.getAppearPosition(2)|" + " " + starAt.appearer.getAppearPosition(2) + " starAt.appearer.getAppearPosition(2) |"  );
            return starAt;
        }
        private void drawLineForGalaxyView(){
            if(nodes[0].value.appearer.activeGO != null && nodes[1].value.appearer.activeGO != null){
                var line = activeGO.GetComponent<DrawLineBetweenPoints>();
                line.setTarget(nodes[0].value.appearer.activeGO, 0);
                line.setTarget(nodes[1].value.appearer.activeGO, 1);
                line.draw();
            }
        }
        private void preAppearFunc(int scene){
            if (scene == 3){
                setPositionForStarView();
            }
        }
        private void postAppearFunc(int scene){
            if (scene == (int)util.Enums.sceneNames.GalaxyView || scene == (int)util.Enums.sceneNames.mainMenu)
            {
                drawLineForGalaxyView();
            }
            if(scene == 3){
                activeGO.GetComponent<starNodeConnectionController>().set(conn);
            }
        }
        public override void destroy(){
            base.destroy();
        }
    }
}
