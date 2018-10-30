using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using System;
public class PlanetClickViewContext : MonoBehaviour {
	public void render(IContextable contextable, Action<IActOnable> ActionsCallBack,Action<IViewable> viewableCallBack ){
		contextable.renderContext( transform, ActionsCallBack,viewableCallBack);
	}

}
