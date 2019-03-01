using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;

namespace Objects.Galaxy
{
[System.Serializable]
    public class StarConnectionAppearer : MultiSceneAppearer
    {
        StarConnectionState connectionState;
        StarConnection conn;
        public StarConnectionAppearer(StarConnection conn, sceneAppearInfo[] sceneToPrefab, StarConnectionState connectionState, AppearableState appearableState) : base(sceneToPrefab, appearableState)
        {
            this.preAppear = preAppearFunc;
            this.postAppear = postAppearFunc;
            this.connectionState = connectionState;
            //TODO have connection be gotten on awake in go;
            this.conn = conn;
        }
        private StarNode setPositionForStarView(){
            var selectedStar = GameManager.instance.selectedStar;
            StarNode starAt;
            StarNode OtherStar;
            if(connectionState.nodes[0].value == selectedStar){
                starAt = connectionState.nodes[0].value;
                OtherStar = connectionState.nodes[1].value; 			
            }else{
                starAt = connectionState.nodes[1].value;
                OtherStar = connectionState.nodes[0].value; 			
            }
            
            Vector3 direction = OtherStar.appearer.state.position - starAt.appearer.state.position;
            state.position  = Vector3.MoveTowards(Vector3.zero,Vector3.zero+direction.normalized*500,500);
            return starAt;
        }
        private void drawLineForGalaxyView(){
            var goA = connectionState.nodes[0].value.appearer.state.appearTransform.gameObject;
            var goB = connectionState.nodes[1].value.appearer.state.appearTransform.gameObject;
            if(goA != null && goB != null){
                var line = state.appearTransform.GetComponent<DrawLineBetweenPoints>();
                line.setTarget(goA, 0);
                line.setTarget(goB, 1);
                line.draw();
            }
        }
        private void preAppearFunc(int scene){
            //todo reset appearstate between nodes depending on active star
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
                state.appearTransform.gameObject.GetComponent<starNodeConnectionController>().set(conn);
            }
        }
        public override void destroy(){
            base.destroy();
        }
    }
}
