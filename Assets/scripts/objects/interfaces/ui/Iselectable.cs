using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI
{
    public interface IControllable{
        InputController controller{get;}
    }
    // public interface ISelectable: IControllable,IUIable{
    // }
    public class inputAction{
        public inputAction(Func<bool> check, Action action){
            this.check = check;
            this.action = action;
        }
        public Func<bool> check;
        public Action action;
    }

}