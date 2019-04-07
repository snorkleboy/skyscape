using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Objects.Galaxy;
using System.Threading.Tasks;
using Objects;
using System.IO;

public class SavedGame{
	public string fileName;
	public string displayName;
	public string data = null;
	public GameManagerModel loadedModel{get;private set;}
	public void loadData(){
		data = System.IO.File.ReadAllText(fileName);
		deserialize();
	}
	public void deserialize(){
		var settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};
		loadedModel = JsonConvert.DeserializeObject<GameManagerModel>(data,settings);
	}
	
}
public static class SavedGameManager {
	public static SavedGame[] getSavedGames(){
		Debug.Log("SAvedGameManger getSavedGames");
		var files = Directory.GetFiles(Constants.Paths.SavedGamePath);

		var list = new SavedGame[files.Length];
		Debug.Log(list + "  " + files.Length);

		for(var i =0;i<files.Length;i++){
			Debug.Log("SAvedGameManger gotFile:" + files[i]);
			var sg = new SavedGame();
			sg.fileName = files[i];
			sg.displayName = files[i].Replace(Constants.Paths.SavedGamePath,"");
			list[i] = sg;
		}
		return list;
	}
	public static void Save(GameManagerModel gmModel) {
		var settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			Formatting = Formatting.Indented
		};
		using (StreamWriter writer = new StreamWriter(Constants.Paths.SavedGamePath + @"\savedGame.json"))
		using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
		{
			JsonSerializer ser = new JsonSerializer();
			ser.TypeNameHandling = TypeNameHandling.Auto;
			//hmmmmm, found kind of late in the game, not sure if it would help unless I can figure out how to get it to auto deserialize unity objects. 
			ser.PreserveReferencesHandling = PreserveReferencesHandling.Objects; 
			ser.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			if (Debug.isDebugBuild)
			{
				ser.Formatting = Formatting.Indented;
			}
			ser.Serialize(jsonWriter, gmModel);
			jsonWriter.Flush();
		}
		Debug.Log("wrote saved game");
	}


}
