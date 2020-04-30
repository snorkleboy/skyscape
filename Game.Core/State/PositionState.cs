using System;
using System.Collections.Generic;
using System.Text;
using Game.Core.Util;
using Game.Core.Entities;
namespace Game.Core.State
{
    public class PositionState
    {
        public Reference<Planet> planetAt;
        public SerializableVector3 position;
        public SerializableQuaternion rotation;

        public PositionState(Reference<Planet> _planetAt, SerializableVector3 _position, SerializableQuaternion _rotation)
        {
            planetAt = _planetAt;
            position = _position;
            rotation = _rotation;
        }
    }
}
