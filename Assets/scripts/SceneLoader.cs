using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace Loaders{
    public static class SceneLoader {

        public static void LoadByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public static Dictionary<string,AssetBundle> loadBundles(){
            var spritesStr = AssetSingleton.bundleNames.sprites;
            var prefabStr  = AssetSingleton.bundleNames.prefabs;
            return new Dictionary<string,AssetBundle>(){
                {spritesStr,loadBundle(spritesStr)},
                {prefabStr,loadBundle(prefabStr)},
            };
        }

        private static AssetBundle loadBundle(string name){
            var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "runtimeLibrary/bundles/"+name));
            if (bundle == null)
            {
                Debug.LogError("Failed to load "+name+" !");
            }
            return bundle;
        }
        public static void loadAssets( Dictionary<string,AssetBundle> bundles){
            foreach (var bundle in bundles)
            {
                bundle.Value.LoadAllAssets();
                AssetSingleton.addBundle(bundle.Key,bundle.Value);
            }
        }
    }

}
