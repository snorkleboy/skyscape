using System.Runtime.Serialization;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using UnityEditor;
using System.Linq;
namespace Objects.Galaxy.State
{
    [System.Serializable]

    public class AppearableContainerState{
        public AppearableContainerState(){}

        public AppearableContainerState(Transform childTransform){
            this.childrenTransform = childTransform;
        }
        public Transform childrenTransform;
        public List<IAppearable> appearables = new List<IAppearable>();
        protected void addAppearable(IAppearable appearable){
            this.appearables.Add(appearable);
        }
        protected void addAppearable(IEnumerable<IAppearable> appearables){
            this.appearables.AddRange(appearables);
        }
        protected void removeAppearable(IAppearable appearable){
            appearables.Remove(appearable);
        }
    }

    [System.Serializable]

    public class FactionOwnedState{
        public Faction belongsTo;
    }
    [System.Serializable]

    public class NamedState{
        public NamedState(){}
        public NamedState(string name){this.name = name;}
        public string name;
    }
    [System.Serializable]
    [DataContract]
    public class AppearableState
    {
        public AppearableState(Transform appearTransform, Vector3 position,Quaternion rotation, StarNode star, bool isActive = false){
            this.appearTransform = appearTransform;
            this.isActive = isActive;
            this.starAt = star;
            this.position = position;
            this.rotation = rotation;
            this.appearTransform.position = position;
        }
        public AppearableState(Transform appearTransform, Vector3 position, StarNode star, bool isActive = false):this(appearTransform,position,Quaternion.identity,star,isActive){

        }
        [DataMember]public Transform activeTransform;
        [DataMember]public Transform appearTransform;
        [DataMember]public SerializableQuaternion _rotation;
        [IgnoreDataMember]public virtual Quaternion rotation {
            get{
                return _rotation;
            }
            set{
                _rotation = value;
                if(activeTransform != null)
                {
                    Debug.Log("setting rotation " + value.y + "  original: " + activeTransform.rotation.y);
                    activeTransform.rotation = value;
                }
            }  
        }
        [DataMember]public SerializableVector3 _position = Vector3.negativeInfinity;
        [IgnoreDataMember]public virtual Vector3 position{
            get{
                return _position;
            }
            set{
                _position = value;
                if(activeTransform != null)
                {
                    activeTransform.position = value;
                }
            }   
        }
        [IgnoreDataMember]public StarNode starAt{get;set;}
        [DataMember]public bool isActive;
    }
}