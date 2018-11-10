using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Objects;
public class PlanetOnClick : MonoBehaviour {
	public PlanetStub planet;
	public PlanetClickView ui; 

	public void Start(){
		var canvas = GameManager.instance.sceneCanvas;
		if (canvas != null){
			ui = canvas.GetComponentInChildren<PlanetClickView>(true);
			if (ui == null){
				Debug.LogWarning("planet on click could not find a PlanetClickView");
			}
		}
	}
	public void OnMouseDown() {
		Debug.Log("planet click");
		ui.set(planet.planet);
	}
}
