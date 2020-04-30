using Game.Core.State;
using Newtonsoft.Json;
namespace Game.Core.State
{
    [System.Serializable]
    public class PlanetState : GalaxyGameObjectState
    {
        [JsonProperty]
        public TileableState tileableState;
        public PlanetState(FactionOwnedState factionState, TileableState tileableState, long id,  NamedState namedState, PositionState positionState, StateActionState actionState) :
        base(id, namedState, positionState, factionState, actionState)
        {
            this.tileableState = tileableState;
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
