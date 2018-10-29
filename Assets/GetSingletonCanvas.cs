using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Util{
    public class GetSingletonCanvas : MonoBehaviour {

        public static GameObject canvas = null;
        public static GameObject getCanvas(){
            return canvas;
        }
        void Awake () {
            Debug.Log("CANVAS SINGELTON AWAKE");


            canvas = transform.gameObject;
            if (canvas == null) {
                Debug.Log("WHAT IS GOING ON CANVAS NULL");
            }else{
                Debug.Log("CANVAS SET");
            }
        }

    }

}
