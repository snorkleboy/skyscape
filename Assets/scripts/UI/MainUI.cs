using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Objects;

public class MainUI : MonoBehaviour {
	public GameObject topbar;
	public GameObject bottomBar;
	public GameObject sideBar;
	private GameManager _manager;
	public void setManager(GameManager manager){
		_manager = manager;
		var userFactionInfo = _manager.user.faction.getIconableInfo();
		Debug.Log("SET MANAGER MAIN UI");
		Debug.Log("userFactionInfo   " + userFactionInfo.name);

		var holder = new GameObject("factionUI");
		var layout = holder.AddComponent<HorizontalLayoutGroup>();
		layout.childControlHeight = true;
		layout.childControlWidth = false;
		// layout.childForceExpandHeight = false;
		// layout.childForceExpandWidth = false;
		holder.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;

		var nameHolder = new GameObject(userFactionInfo.name);
		var text = nameHolder.AddComponent<Text>();
		text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text.text = userFactionInfo.name;
		nameHolder.transform.SetParent(holder.transform);
		if (userFactionInfo.details != null){
			foreach (var subInfo in userFactionInfo.details )
			{
				var detail = subInfo.name;
				var detailHolder = new GameObject(detail);
				text = detailHolder.AddComponent<Text>();
				text.text = detail;
				text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
				detailHolder.transform.SetParent(holder.transform);
			}
		}
		holder.transform.SetParent(topbar.transform);
		Debug.Log("MAIN UI SET");

	}
}
