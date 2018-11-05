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
            canvas = transform.gameObject;
        }

    }

}
