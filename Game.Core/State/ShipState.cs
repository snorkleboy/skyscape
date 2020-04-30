using Newtonsoft.Json;
using System.Runtime.Serialization;
using Game.Core.Entities;
namespace Game.Core.State
{
    [System.Serializable]
    [DataContract]

    public class ShipState : GalaxyGameObjectState
    {
        public Fleet fleetShipIsIn;
        [DataMember] public Weapon[] weapons;
        [DataMember] public DestructableState destructableState;
        [DataMember] public ShieldedState shieldedState;
        public int ShipSize = 1;
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
