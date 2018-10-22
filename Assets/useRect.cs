using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class useRect : MonoBehaviour {
    public RectTransform rectTarget;
    public Camera camera;
    public RectTransform thisRect;
	// Update is called once per frame
	void Update () {
        camera.pixelRect = rectTarget.rect;
        camera.rect = rectTarget.rect;

    }
}
