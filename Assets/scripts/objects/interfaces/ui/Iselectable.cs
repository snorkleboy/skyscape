using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI
{
    public interface ISelectable: IUIable{
        InputController getInputController(GameObject parent);
    }
    public struct inputAction{
        public inputAction(Func<bool> check, Action action){
            this.check = check;
            this.action = action;
        }
        public Func<bool> check;
        public Action action;
    }

}