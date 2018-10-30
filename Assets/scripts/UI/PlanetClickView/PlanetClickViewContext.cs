using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using System;
public class PlanetClickViewContext : MonoBehaviour {

	public void render(IContextable contextable){
		contextable.renderContext( transform, b,a);
	}
	public void a(IViewable a){

	}
	public void b(IActOnable b){

	}
}
