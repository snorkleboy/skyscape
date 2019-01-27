using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProvider : MonoBehaviour {

	public static Transform canvas;
	public void OnLevelWasLoaded(){
		Debug.Log("canvas provider loaded");
		canvas = this.transform;
	}

}
