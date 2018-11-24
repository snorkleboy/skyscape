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

		public void render(IViewable viewable, clickViews viewCallBacks){
			Debug.Log("render MainView " + viewable);
			clear(centralView);
			viewable.renderUIView(centralView.transform,viewCallBacks);
		}
		public void clearMain(){
			clear(centralView);
		}
		private void clear(GameObject gameObject){
			foreach(Transform child in gameObject.transform){
				Destroy(child.gameObject);
			}
		}
	}
}

