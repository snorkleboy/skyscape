using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProvider : MonoBehaviour {

	public static Transform canvas;
	public void OnLevelWasLoaded(){
		canvas = this.transform;
	}

}
