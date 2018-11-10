using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
	public bool rotate = true;
	void Update () {
		transform.LookAt(Camera.main.transform);
		if (rotate){
			transform.Rotate(0,180,0);
		}
	}
}
