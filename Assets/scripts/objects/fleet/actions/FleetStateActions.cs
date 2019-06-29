using UnityEngine;
using System.Collections.Generic;
namespace Objects
{
    public static class FleetStateActions{
        public static StateAction moveFleet(Fleet fleet,Vector3 target){
            return new MoveFleet().init(fleet, target);
        }
        public static StateAction moveFleetBetweenPoints(Fleet fleet,Vector3[] targets){
            return new MoveFleetBetweenPoints().init(fleet, targets);
        }
        public static class Patrols{
            public static StateAction patrolFleet(Fleet fleet,Vector3[] targets){
                return new PatrolFleet(){onFindFleet=engageFoundFleets}
                .init(fleet, targets);
            }

            private static object engageFoundFleets(List<Fleet> foundFleets,Fleet controlledFleet){
                return new EngageFleet().init(controlledFleet,foundFleets[0]);
            }
        }


    }

}

