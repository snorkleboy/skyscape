using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using GalaxyCreators;
using System.IO;
using UI;

namespace UI
{
    public class InputController: MonoBehaviour{
        // public Dictionary<string,Sprite> cursorIcons;
        [SerializeField]
        public List<inputAction> controls;
        public GameObject source;
        public void Init(List<inputAction> controls, GameObject source){
            this.controls = controls;
            this.source = source;
        }
        public void Update(){
            if(controls != null){
                foreach (var control in controls)
                {
                    var check = control.check();
                    if(check){
                        control.action();
                    }
                }
            }
        }
    }
}