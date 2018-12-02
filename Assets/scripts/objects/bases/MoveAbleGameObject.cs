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
        public Objects.StateAction stateAction = null;
        public void setStateAction(StateAction action){
            Debug.Log("set state action " + action);
            this.stateAction = action;
        }
        public Objects.StateAction previousAction = null;
        public virtual IRenderer renderHelper{get;set;} 
        public abstract void render(int scene);
        public abstract IMover mover{get;}
        public Sprite icon;
        public string title;
        public abstract iconInfo getIconableInfo();

        public virtual void Update(){
            if(stateAction != null){
                if(!stateAction.MoveNext()){
                    Debug.Log("not move next " + stateAction);
                    previousAction = stateAction;
                    stateAction = null;
                }
            }
        }
    }
}
