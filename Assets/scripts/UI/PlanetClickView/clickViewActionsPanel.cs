using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI{
	public class clickViewActionsPanel : MonoBehaviour {
		public void render(IActOnable actOnable){
			actOnable.renderActionView(transform);
		}
	}
}

