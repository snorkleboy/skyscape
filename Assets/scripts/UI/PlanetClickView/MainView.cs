using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using System;
namespace UI{
	public class MainView : MonoBehaviour {

		public GameObject topbar;
		public GameObject centralView;
		public GameObject bottomBar;

		public void render(IViewable viewable, Action<IContextable> contextCB){
			Debug.Log("render MainView " + viewable);
			viewable.renderUIView(centralView.transform,contextCB);
		}
	}
}

