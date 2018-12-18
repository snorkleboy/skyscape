﻿using System.Collections;
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
		starNode = GetComponentInParent<StarNode>();
		if(starNode == null){
			Debug.Log("starNode not found in starconnection");
		}
		if(starConnection.nodes[0] == starNode){
			other = starConnection.nodes[1]; 			
		}else{
			other = starConnection.nodes[0]; 			
		}
		var name = other.name;
		text.text = name;
	}
	void OnMouseDown(){
		GameManager.instance.loadStarSystem(other);
	}
}
