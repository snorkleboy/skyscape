using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace UI
{
	public class PlanetClickView : BaseUIScript{
		public MainView mainview;
		public PlanetClickViewContext contextPane;
		public GameObject actionPane;

		protected override void refresh(){
			if (lastUpdateId != _toDisplay.updateId){
				render();
			}
		}
		protected override void render(){
			if (_toDisplay != null){
				lastUpdateId = _toDisplay.updateId;
				mainview.render(_toDisplay,renderContext);
			}else{
				Debug.LogWarning(this + " called render without _toDisplay set  ");
			}
		}
		protected void renderContext(IContextable tile){
			Debug.Log("renderContext callback called, proof:" + tile);
			contextPane.render(tile);
		}
	}

}
