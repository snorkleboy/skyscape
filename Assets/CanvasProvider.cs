using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProvider : MonoBehaviour {

	public static Transform canvas;
	public void OnLevelWasLoaded(){
		Debug.Log("CANVAS PROVIDER AWAKE");
		canvas = this.transform;
	}

}
