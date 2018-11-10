using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Objects.Galaxy;
using Objects;
public class starNodeConnectionController : MonoBehaviour {

	StarConnection starConnection;
	StarNode starNode;
	StarNode other;
	public TextMesh text;
	public void set (StarConnection starConnection) {
		var starNodeStub = GetComponentInParent<StarStub>();
		starNode = starNodeStub.starnode;
		if(starNode == null){
			Debug.Log("starNode stub not found in starconnection");
		}
		var name = "";
		StarNode here;
		if(starConnection.nodes[0] == starNode){
			here = starConnection.nodes[0];
			other = starConnection.nodes[1]; 			
		}else{
			here = starConnection.nodes[1];
			other = starConnection.nodes[0]; 			
		}
		name = other.name;
		Vector3 direction = other.position - here.position;
		transform.position = starNodeStub.transform.position;
		transform.Translate((direction/direction.magnitude)*500);
		text.text = name;
	}
	void OnMouseDown(){
		GameManager.instance.loadStarSystem(other);
	}
}
