using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace UI
{
	public class PlanetClickView : BaseUIScript<Planet>{
		public MainView centralPane;
		public GameObject contextPane;
		public GameObject actionPane;

		protected override void refresh(){
			if (lastUpdateId != _toDisplay.updateId){
				render();
			}
		}
		protected override void render(){
			if (_toDisplay != null){
				lastUpdateId = _toDisplay.updateId;
				centralPane.render(_toDisplay);
			}else{
				Debug.LogWarning(this + " called render without _toDisplay set  ");
			}
		}
	}

}
