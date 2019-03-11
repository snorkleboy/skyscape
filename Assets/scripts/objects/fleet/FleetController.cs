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

                
            // this.name = name;
            // this.state.icon = icon;
            // this.fleetPosition = position;
            // this._appearer = renderHelper;
            // var fleetMover = gameObject.AddComponent<FleetMover>();
            // ships = new ShipManager(fleetMover);
            // owningFaction = faction;

            
        // public void appear(int scene){
            // foreach (var appearable in _appearer.appearables){
            //     var appearPos = fleetPosition + new Vector3(1 + 3*count++,0,0);
            //     appearable.appearer.setAppearPosition(appearPos,3);
            // }
            // if(appearer.appear(scene)){
            //     appearer.activeGO.transform.position = fleetPosition;
            //     this.ships.mover.fleetTransform = appearer.activeGO.transform;
            // }
        // }