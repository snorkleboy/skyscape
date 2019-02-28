
    using System.Data;
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

    public class AppearableContainerState{
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


    public class FactionOwnedState{
        public Faction belongsTo;
    }
    public class NamedState{
        public NamedState(string name){this.name = name;}
        public string name;
    }

    public class AppearableState
    {
        public AppearableState(Transform appearTransform, Vector3 position, StarNode star, bool isActive){
            this.appearTransform = appearTransform;
            this.isActive = isActive;
            this.starAt = star;
            this.position = position;
        }
        public Transform appearTransform { set;get; }
        private Vector3 _position = Vector3.negativeInfinity;
        public virtual Vector3 position{
            get{
                return _position;
            }
            set{
                _position = value;
                if(isActive){
                    appearTransform.position = _position;
                }
            }   
        }
        public StarNode starAt{get;set;}
        public bool isActive{get;set;}
    }
}