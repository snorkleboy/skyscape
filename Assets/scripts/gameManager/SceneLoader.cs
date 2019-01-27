using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Objects.Galaxy;
using System.Threading.Tasks;
using Objects;
using GalaxyCreators;
using Objects.Conceptuals;

namespace Loaders {
    public class SceneLoader : MonoBehaviour {
        public static GameManager gameManager;
        IEnumerator hydrateCallBack;
        private static Dictionary<string, AssetBundle> bundles = null;
        private static void Log(string msg, Text txt){
            Debug.Log(msg);
            txt.text = msg;
        }
        public async void onLoadingScreen(Scene scene, LoadSceneMode mode){
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= onLoadingScreen;
            StartCoroutine(buildGameRoutine(hydrateCallBack));
        }

        public void buildGame(GameManager gameManager, IEnumerator hydrateCallBack){
            SceneLoader.gameManager = gameManager;
            if(gameManager.UIManager != null){
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= gameManager.UIManager.getSceneCanvas;
            }
            this.hydrateCallBack = hydrateCallBack;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += onLoadingScreen;
            SceneLoader.LoadByIndex(1);
        }
        public static IEnumerator buildGameRoutine(IEnumerator hydrateCallBack){
            Debug.Log("loading screen loaded");

            unloadBundlesSync();
            var spritesStr = AssetSingleton.bundleNames.sprites;
            var prefabStr = AssetSingleton.bundleNames.prefabs;
            bundles = new Dictionary<string, AssetBundle> () { { spritesStr,null  }, { prefabStr,null }};

            var textEl = GameObject.Find("Text").GetComponent<Text>();
            if(!textEl){
                Debug.LogError("startGame cant find text");
            }
            textEl.text = "loading";
            yield return new WaitForSeconds(.1f);

            Log("loading bundles",textEl);
            if (!Application.isEditor){
                Log("loading bundles: "+spritesStr,textEl);
                yield return loadBundle(spritesStr);
                Log("loading bundles: "+prefabStr,textEl);
                yield return loadBundle(prefabStr);
            }else{
                Log("loading bundles: "+spritesStr,textEl);
                loadBundleSync(spritesStr);
                Log("loading bundles: "+prefabStr,textEl);
                loadBundleSync(prefabStr);
            }
            yield return new WaitForSeconds(.1f);

            Log("loading assets ",textEl);
            SceneLoader.loadAssets();
            yield return new WaitForSeconds(.1f);

            Log("Making Factories",textEl);
            // gameManager.shipFactory = gameManager.gameObject.AddComponent<ShipFactory>();
            var creator = GameObject.Instantiate(gameManager.GameCreatorPrefab);
            gameManager.galaxyCreator = creator.GetComponentInChildren<GameGalaxyCreator>();
            creator.SetActive(true);
            var galHolder = GameObject.Find("Galaxy");
            galHolder.transform.SetParent(gameManager.transform);
            yield return new WaitForSeconds(.1f);
            
            Log("Hydrating Galaxy",textEl);
            yield return new WaitForSeconds(.1f);
            yield return hydrateCallBack;
            yield return new WaitForSeconds(.1f);

            Log("setting up UI",textEl);
            gameManager.UIManager.setGameManager(gameManager);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += gameManager.UIManager.getSceneCanvas;
            yield return new WaitForSeconds(.1f);
            Log("loading galaxy scene",textEl);
            LoadByIndex(2);
            yield return new WaitForSeconds(.1f);
            Debug.Log("rendering galaxy");
            gameManager._starNodes.render(2);
            gameManager.UIManager.mainUI.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }

        public static void onStarLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded=>onStarLoaded()=>renderStar(star): scene.name:" + scene.name + " selectedStar:" + gameManager.selectedStar);
            gameManager.selectedStar.appear((int)util.Enums.sceneNames.StarSystemView);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= onStarLoaded;
        }
        public static void loadStarSystem(StarNode star){
            Debug.Log("loading star system");
            gameManager._starNodes.destroy();
            SceneLoader.LoadByIndex(3);
            gameManager.selectedStar = star;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += onStarLoaded;
        }
        public static void renderGalaxyView(){
            SceneLoader.LoadByIndex(2);
            gameManager._starNodes.render(2);
        }
        public static void LoadByIndex (int sceneIndex) {
            SceneManager.LoadScene (sceneIndex);
        }

        private static IEnumerator loadBundle (string name) {
            string path;
            path = Application.dataPath+"/" +"StreamingAssets/bundles/"+name;

            var bundleLoadRequest  = AssetBundle.LoadFromFileAsync(path);
            yield return bundleLoadRequest;
            var bundle = bundleLoadRequest.assetBundle;
            if (bundle == null) {
                Debug.LogError ("Failed to load " + name + " !    assetPaths:" + path + "\n    Application.dataPath    " + Application.dataPath);
            }
            bundles[name] = bundle;
        }
        private static AssetBundle loadBundleSync(string name) {
            string path;
            path = Application.dataPath+"/" +"StreamingAssets/bundles/"+name;

            var bundle  = AssetBundle.LoadFromFile(path);
            if (bundle == null) {
                Debug.LogError ("Failed to load " + name + " !    assetPaths:" + path + "\n    Application.dataPath    " + Application.dataPath);
            }
            bundles[name] = bundle;
            return bundle;
        }
        private static void unloadBundlesSync() {
            if(bundles != null){
                foreach(var keyVal in bundles){
                    keyVal.Value.Unload(false);
                }
            }
            
        }
        public static void loadAssets () {
            foreach (var bundle in bundles) {
                AssetSingleton.addBundle (bundle.Key, bundle.Value);
            }
        }
    }

}