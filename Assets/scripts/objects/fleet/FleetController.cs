using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Objects
{
    public class FleetController : InputController{
        FleetMover mover;
        public FleetController init(FleetMover mover){
            this.mover = mover;
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
            Debug.Log("set position " + this);
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                var newvec = hit.point;
                newvec.y = 0;
                mover.moveTo(newvec);
            }else{
                Debug.Log("no hit on move to click");
            }
        }
    }
}