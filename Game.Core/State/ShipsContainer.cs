using Newtonsoft.Json;
using System.Collections.Generic;
using Game.Core.Entities;
using Game.Core.Util;
namespace Game.Core.State
{

    [System.Serializable]
    public class DockableShipContainer : ShipsContainer
    {
        [JsonProperty] public int dockSize;
        [JsonProperty] public int dockGridSize;
        [JsonProperty] public List<Reference<Ship>> dockedShips;
    }

    [System.Serializable]
    public class ShipsContainer
    {

        [JsonProperty] public List<Reference<Ship>> ships = new List<Reference<Ship>>();
        public System.Action onEmpty;

        public virtual bool removeShip(Ship ship)
        {
            this.ships.Remove(ship);
            //this.appearables.Remove(ship);
            if (this.ships.Count == 0 && onEmpty != null)
            {
                onEmpty();
            }
            return true;
        }
        public virtual bool addShips(Ship ship)
        {
            this.ships.Add(ship);
            //this.appearables.Add(ship);
            return true;
        }
        public virtual bool addShips(List<Ship> ships)
        {
            this.ships.AddRange(ships.referenceAll());
            //this.appearables.AddRange(ships);
            return true;
        }
    }


    //[Serializable]
    //[DataContract]
    //[JsonObject(MemberSerialization.OptOut)]

    //public class AppearablePositionState
    //{
    //    public AppearablePositionState() { }
    //    public AppearablePositionState(Transform appearTransform, Vector3 position, Quaternion rotation, StarNode star, bool isActive = false)
    //    {
    //        this.appearTransform = appearTransform;
    //        this.isActive = isActive;
    //        this.starAt = (Reference<StarNode>)star;
    //        this.position = position;
    //        this.rotation = rotation;
    //        this.appearTransform.position = position;
    //    }
    //    public AppearablePositionState(Transform appearTransform, Vector3 position, StarNode star, bool isActive = false) : this(appearTransform, position, Quaternion.identity, star, isActive)
    //    {

    //    }
    //    [JsonIgnoreAttribute] [DataMember] public Transform activeTransform;
    //    [JsonIgnoreAttribute] [DataMember] public Transform appearTransform;
    //    [DataMember] public SerializableQuaternion _rotation;
    //    [JsonIgnoreAttribute]
    //    [IgnoreDataMember]
    //    public virtual Quaternion rotation
    //    {
    //        get
    //        {
    //            return _rotation;
    //        }
    //        set
    //        {
    //            _rotation = value;
    //            if (activeTransform != null)
    //            {
    //                activeTransform.rotation = value;
    //            }
    //        }
    //    }
    //    [DataMember] public SerializableVector3 _position = Vector3.negativeInfinity;
    //    [JsonIgnoreAttribute]
    //    [IgnoreDataMember]
    //    public virtual Vector3 position
    //    {
    //        get
    //        {
    //            return _position;
    //        }
    //        set
    //        {
    //            _position = value;
    //            if (activeTransform != null)
    //            {
    //                activeTransform.position = value;
    //            }
    //        }
    //    }
    //    [IgnoreDataMember] public Reference<StarNode> starAt { get; set; }
    //    [JsonIgnoreAttribute] [DataMember] public bool isActive = false;
    //}
}
