using System.Runtime.Serialization;
using System.Linq;
using System.Reflection;
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
	public SaveGameModel loadedModel{get;private set;}
	public void loadData(){
		data = System.IO.File.ReadAllText(fileName);
		deserialize();
	}
	public void deserialize(){
		var data = System.IO.File.ReadAllText(Constants.Paths.SavedGamePath + @"\savedGame.json");
		var settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
		};
		loadedModel = JsonConvert.DeserializeObject<SaveGameModel>(data,settings);
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
	public static void Save(SaveGameModel gmModel) {
		_Save(gmModel,@"\savedGame.json");
	}

	public static void _Save(SaveGameModel gmModel,string fileName) {
		DateTime start = DateTime.Now;

		using (StreamWriter writer = new StreamWriter(Constants.Paths.SavedGamePath + fileName))
		using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
		{
			JsonSerializer ser = new JsonSerializer();
			ser.TypeNameHandling = TypeNameHandling.Auto;
			if (Debug.isDebugBuild)
			{
				ser.Formatting = Formatting.Indented;
			}
			ser.Serialize(jsonWriter, gmModel);
			jsonWriter.Flush();
		}
		DateTime end = DateTime.Now;
		Debug.Log("wrote saved game" + " " + (end-start));
	}


}
