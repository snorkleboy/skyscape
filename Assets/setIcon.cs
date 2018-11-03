using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
namespace UI{
	public class setIcon : MonoBehaviour {

		public Text text;
		public static GameObject panelPreFab;        
		public void Start(){
		}

		public void setUIHelperIcon(IIconable iconable){
			var panel= Instantiate(panelPreFab);
			iconable.renderIcon().transform.SetParent(panel.transform,false);
			text.text = iconable.title;
		}
	}
}
