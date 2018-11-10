using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneEnabled : MonoBehaviour {
	public int enabledOn;
	
	private void OnLevelWasLoaded(){
		var scene = SceneManager.GetActiveScene();
		if (scene.buildIndex == enabledOn){
			gameObject.SetActive(true);
		}else{
			gameObject.SetActive(false);
		}
	}
}
