using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
public class SpaceableIcon : MonoBehaviour {

	IUIable obj;
	public MeshRenderer mesh;

	public void set(IUIable infoable){
		obj = infoable;
		Debug.Log("SET SPACEABLE ICON");
		mesh.materials[0].mainTexture = obj.getIconableInfo().icon.texture;
	}
}
