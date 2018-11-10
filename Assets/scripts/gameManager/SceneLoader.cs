using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Loaders {
    public static class SceneLoader {

        public static void LoadByIndex (int sceneIndex) {
            SceneManager.LoadScene (sceneIndex);
        }

        public static Dictionary<string, AssetBundle> loadBundles () {
            var spritesStr = AssetSingleton.bundleNames.sprites;
            var prefabStr = AssetSingleton.bundleNames.prefabs;
            return new Dictionary<string, AssetBundle> () { { spritesStr, loadBundle (spritesStr) }, { prefabStr, loadBundle (prefabStr) },
            };
        }

        private static AssetBundle loadBundle (string name) {
            string path;
            path = Path.Combine(Application.dataPath, "StreamingAssets/bundles/"+name);
            var bundle = AssetBundle.LoadFromFile (path);
            if (bundle == null) {
                Debug.LogError ("Failed to load " + name + " !    assetPaths:" + path + "    Application.dataPath    " + Application.dataPath + "Application.streamingAssetsPath"      + Path.Combine(Application.streamingAssetsPath, "/bundles/" + name));
            }
            return bundle;
        }
        public static void loadAssets (Dictionary<string, AssetBundle> bundles) {
            foreach (var bundle in bundles) {
                AssetSingleton.addBundle (bundle.Key, bundle.Value);
            }
        }
    }

}