using UnityEngine;

namespace Objects.Galaxy.ship
{
    public static class ShipStateActions{

        public static StateAction moveToPoint(this Ship ship, Vector3 target)
        {
            return new MoveToPoint().Init(ship.state.positionState, 15, 2, target);
        }
        public static StateAction followReference(this Ship ship, State.AppearablePositionState target)
        {
            return new FollowReference().Init(ship.state.positionState, target, 15f, 30);
        }
        public static StateAction engageShip(this Ship ship, Ship target, System.Func<object> onDestroyTarget)
        {
            return new EngageShip().Init(ship, target, onDestroyTarget);
        }

    }
}