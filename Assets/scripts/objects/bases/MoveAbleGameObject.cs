using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public abstract class MoveAbleGameObject:MonoBehaviour,IUIable,IRenderable,IMoveable
    {
        public virtual IRenderer renderHelper{get;set;} 
        public abstract void render(int scene);
        public abstract IMover mover{get;}
        public Sprite icon;
        public string title;
        public abstract iconInfo getIconableInfo();
    }
}
