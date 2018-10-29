using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace UI{
	public class MainView : MonoBehaviour {

		public GameObject topbar;
		public GameObject centralView;
		public GameObject bottomBar;

		public void render(Planet planet){
			planet.tileManager.renderTileView(centralView.transform);
		}
	}
}

