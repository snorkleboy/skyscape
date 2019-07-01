using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
public class LoadGameView : MonoBehaviour {
	public GameObject listGo;
	public StartGame gameStarter;
	public bool loadGameMode=false;
	public void setMode(bool b){
		loadGameMode = b;
	}
	
	public Button actionButton;
	public InputField inputField;

	private SavedGame activeSaveGame;
	private string activeString;
	private List<GameObject> savedGamesGos = new List<GameObject>();

	// Use this for initialization
	void OnEnable()
	{
		displaySavedGames(SavedGameManager.getSavedGames());
		inputField.onValueChanged.AddListener((str)=>activeString = str);
		string msg;
		if(loadGameMode){
			msg = "load";
		}else{
			msg = "save";
		}
		actionButton.GetComponentInChildren<Text>().text = msg;
		actionButton.onClick.AddListener(()=>{var a=loadGameMode? loadGame(): saveGame();});
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
			button.onClick.AddListener(()=>onSavedGameClick(SGItem,saved));
			var text = SGItem.AddComponent<Text>();
			var trn = SGItem.GetComponent<RectTransform>();
			trn.sizeDelta = new Vector3(120,30);
			text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			text.text = saved.displayName;

		}
	}
	public void onSavedGameClick(GameObject go,SavedGame savedGame){
		Debug.Log("CLICK SG " + savedGame.fileName);
		activeString = savedGame.displayName;
		inputField.text = activeString;
		activeSaveGame = savedGame;

	}
	public bool loadGame(){
		if(activeSaveGame != null){
			activeSaveGame.loadData();
			gameStarter.startGame(activeSaveGame);
			listGo.transform.parent.gameObject.SetActive(false);
		}else{
			Debug.LogError("No saved game selected, clicked load");
		}

		return true;
	}
	public bool saveGame(){
		GameManager.instance.Save(activeString);
		return true;
	}

}
