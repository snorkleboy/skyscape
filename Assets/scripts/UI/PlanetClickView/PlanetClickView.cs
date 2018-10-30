using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace UI
{
	public class PlanetClickView : BaseUIScript{
		public MainView mainview;
		public GameObject contextPane;
		public GameObject actionPane;

		protected override void refresh(){
			if (lastUpdateId != _toDisplay.updateId){
				render();
			}
		}
		protected override void render(){
			if (_toDisplay != null){
				renderMain(_toDisplay);
			}else{
				Debug.LogWarning(this + " called render without _toDisplay set  ");
			}
		}
		protected void renderMain(IViewable viewable){
			lastUpdateId = viewable.updateId;
			mainview.render(_toDisplay,renderContext);
		}
		protected void renderContext(IContextable contextable){
		Debug.Log("renderContext callback called, proof:" + contextable);
			contextable.renderContext(contextPane.transform,renderActions,renderMain);
		}
		protected void renderActions(IActOnable actionable){
			Debug.Log("renderActions callback called, proof:" + actionable);
			actionable.renderActionView(actionPane.transform);
		}
	}

}
