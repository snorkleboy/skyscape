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
	public void loadData(){
		data = System.IO.File.ReadAllText(fileName);
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
		Debug.Log("writing file");
		var gameJson = JsonConvert.SerializeObject(gmModel, Formatting.Indented);
		Task.Run(()=>{
			File.WriteAllText(Constants.Paths.SavedGamePath + "\\savedGame.json",gameJson);
			Debug.Log("writing file done");
		});
	}

}
