using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleOn : MonoBehaviour {

	public GameObject gameObject;
	public bool active;
	public void toggle() {
		active=!active;
		gameObject.SetActive(active);
	}
}
