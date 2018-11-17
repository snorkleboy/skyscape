using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Objects;
public class PlanetOnClick : MonoBehaviour {
	public Planet planet;
	public ClickViewDetailPanel ui; 

	public void Start(){
		ui = GameManager.instance.GMcanvas.GetComponentInChildren<ClickViewDetailPanel>();
		if (ui == null){
			Debug.LogWarning("planet on click could not find a PlanetClickView");
		}
		planet = gameObject.GetComponentInParent<Planet>();
		if (planet == null){
			Debug.LogWarning("planet on click could not find a Planet");
		}
	}
	public void OnMouseDown() {
		Debug.Log("planet click");
		ui.set(planet);
	}
}
