using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Objects
{
    public class FleetController : InputController{
        public Fleet fleet;
        public FleetController init(Fleet fleet){
            this.fleet = fleet;
            controls = new List<inputAction>()
                {
                    new inputAction(checkClick,moveTo),
                };
            return this;
        }
        public bool checkClick(){
            return Input.GetMouseButtonDown(1);
        }
        public void moveTo(){
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                var newvec = hit.point;
                newvec.y = 0;
                fleet.setStateAction(fleet.move(newvec));
            }else{
                Debug.Log("no hit on move to click");
            }
        }
    }
}