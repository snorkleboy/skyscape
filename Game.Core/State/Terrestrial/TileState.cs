using Game.Core.Entities;
using Game.Core.Util;
using System.Collections.Generic;

namespace Game.Core.State
{
    public class TileState : TerrestrialState
    {
        public Reference<Building> building = null;
        public int tilePosition;

        public void setBuilding(Building building)
        {
            this.building = (Reference<Building>)building;
        }

    }
}
