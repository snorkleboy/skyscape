using UnityEngine;

namespace Objects
{
    public static class FleetStateActions{
        public static StateAction moveFleet(Fleet fleet,Vector3 target){
            return new MoveFleet().init(fleet, target);
        }
        public static StateAction moveFleetBetweenPoints(Fleet fleet,Vector3[] targets){
            return new MoveFleetBetweenPoints().init(fleet, targets);
        }
        public static StateAction patrolFleet(Fleet fleet,Vector3[] targets){
            return new PatrolFleet(){onFindFleet=(foundFleets)=>new EngageFleet().init(fleet,foundFleets[0])}
            .init(fleet, targets);
        }
    }

}

