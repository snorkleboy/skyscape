using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Objects
{
    public class FleetController : InputController{

        public Fleet fleet;
        private Texture2D defaultCursor = GameManager.uiManager.moveCursor;

        private MonoBehaviour hoveredObj;
        private Fleet hoveredEnemyFleet = null;
        public FleetController init(Fleet fleet){
            this.fleet = fleet;
            controls = new List<inputAction>()
                {
                    new inputAction(checkClick,rightClick),
                };
            return this;
        }
        public override InputController startControl()
        {
            GameManager.uiManager.setCursorTexture(defaultCursor);
            return this;
        }
        public override void endControl()
        {
            GameManager.uiManager.resetCursorTexture();
        }
        public override void hoverResponse(MonoBehaviour hoveredObject)
        {
            if (hoveredObject)
            {
                var fleet = hoveredObject as Fleet;

                if (!fleet?.isUsers() ?? false)
                {
                    hoveredEnemyFleet = fleet;
                    this.hoveredObj = hoveredObject;
                    GameManager.uiManager.setCursorTexture(GameManager.uiManager.attackCursor);
                }
            }
            else
            {
                hoveredEnemyFleet = null;
                this.hoveredObj = null;
                GameManager.uiManager.setCursorTexture(defaultCursor);
            }

        }
        public bool checkClick(){
            return Input.GetMouseButtonDown(1);
        }
        public void rightClick(){

            if (hoveredObj)
            {
                Debug.Log(fleet.FleetName() + " clicked on " + hoveredObj + " " + hoveredEnemyFleet?.FleetName());
                if(hoveredEnemyFleet){
                    fleet.setStateAction(fleet.engageFleet(hoveredEnemyFleet));
                }
            }
            else
            {
                moveToMouse();
            }
        }
        public void moveToMouse()
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                var newvec = hit.point;
                newvec.y = 0;
                fleet.setStateAction(fleet.move(newvec));
            }
            else
            {
                Debug.Log("no hit on move to click");
            }
        }
    }

}