using UnityEngine;

namespace Objects
{
    public static class FleetStateActions{
            public static StateAction moveFleet(Fleet fleet,Vector3 target){
                return new MoveFleet().init(fleet, target);
            }
    }

}

