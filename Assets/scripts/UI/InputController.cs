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
    public class InputController : MonoBehaviour{
        // public Dictionary<string,Sprite> cursorIcons;
        [SerializeField]
        public List<inputAction> controls;
        public static void checkAction(InputController controller){
            if(controller.controls != null){
                foreach (var control in controller.controls)
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