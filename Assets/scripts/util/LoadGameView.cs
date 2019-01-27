using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
public class LoadGameView : MonoBehaviour {
	public GameObject listGo;
	public StartGame gameStarter;
	public List<GameObject> savedGamesGos = new List<GameObject>();
	// Use this for initialization
	void OnEnable()
    {
		displaySavedGames(SavedGameManager.getSavedGames());
    }
	void OnDisable()
    {
		clear();
    }
	public void clear(){
		foreach (var item in savedGamesGos)
		{
			Destroy(item);
		}
	}

	public void displaySavedGames(SavedGame[] savedGames){
		Debug.Log("displaySavedGames");
		foreach (var saved in savedGames)
		{
			Debug.Log(saved.displayName);
			var SGItem = new GameObject(saved.displayName);
			savedGamesGos.Add(SGItem);
			SGItem.SetParent(listGo);
			var button = SGItem.AddComponent<Button>();
			button.onClick.AddListener(()=>onSavedGameClick(saved));
			var text = SGItem.AddComponent<Text>();
			text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			text.text = saved.displayName;
		}
	}
	public void onSavedGameClick(SavedGame savedGame){
		savedGame.loadData();
		gameStarter.startGame(savedGame);
		listGo.transform.parent.gameObject.SetActive(false);
	}

}
