using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Util{
public class getPlanetClickViewSingle : MonoBehaviour {
	public static GameObject obj = null;
	public static UI.PlanetClickView clickView;
	void Awake () {
		Debug.Log("clickView singelton getter awake");
		obj = transform.gameObject;
		clickView = obj.GetComponent<UI.PlanetClickView>();
		if (clickView == null){
			Debug.Log("clickView singelton click view null ");
		}
	}
}

}
