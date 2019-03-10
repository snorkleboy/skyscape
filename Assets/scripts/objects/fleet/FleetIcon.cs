using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Objects;
using UI;
public class FleetIcon : SpaceIcon {
    Fleet fleet;
    public override void Start(){
        fleet = gameObject.GetComponentInParent<Fleet>();
        uiable = fleet;
        if (fleet == null){
            Debug.LogWarning("fleetIcon did not find fleet");
        }
    }
    protected override Vector3 getTargetPosition(){
        return fleet.state.positionState.position;
    }
    protected override bool shouldRender(){
        return true;
    }
}