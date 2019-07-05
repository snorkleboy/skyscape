using UnityEngine;
using System.Collections.Generic;
namespace Objects
{
    public static class ActionExtensions
    {
        public static IActionable setStateAction(this IActionable actionable, StateAction action)
        {
            actionable.stateActionState
                .setStateAction(action);
            return actionable;
        }
        //TODO make moveTOPoint into something that uses Moveable
//        public static IMoveable moveTo(this IMoveable moveable, Vector3 target, float d = .5f)
//        {
//            moveable.stateActionState
//                .setStateAction(new Objects.Galaxy.MoveToPoint().Init(moveable.positionState, 15f, d, target));
//            return moveable;
//        }

    }
    public static class FleetStateActions{

        public static Fleet setStateAction(this Fleet fleet, StateAction action)
        {

            fleet.state.setStateAction(action);
            return fleet;
        }
        public static StateAction engageFleet(this Fleet fleet, Fleet toEngage)
        {
            return new EngageFleet()
                .init(fleet, toEngage);
        }
        public static StateAction move(this Fleet fleet,Vector3 target){
            return new MoveFleet()
                .init(fleet, target);
        }
        public static StateAction moveBetweenPoints(this Fleet fleet,Vector3[] targets){
            return new MoveFleetBetweenPoints()
                .init(fleet, targets);
        }

        public static StateAction patrol(this Fleet fleet, Vector3[] targets)
        {
            return new PatrolFleet() { onFindFleet = engageFoundFleets }
                .init(fleet, targets);
        }

        private static object engageFoundFleets(List<Fleet> foundFleets, Fleet controlledFleet)
        {
            Fleet enemyFleet = null;
            for(var i = 0; i < foundFleets.Count; i++)
            {
                var fleet = foundFleets[i];
                if(fleet.state.factionOwnedState.belongsTo.id != controlledFleet.state.factionOwnedState.belongsTo.id)
                {
                    enemyFleet = fleet;
                    break;
                }
            }
            if (enemyFleet)
            {
                return new EngageFleet()
                .init(controlledFleet, enemyFleet);
            }
            else
            {
                return null;
            }
    
        }
    }

}

