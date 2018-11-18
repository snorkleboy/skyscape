﻿using System.Collections;
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
        public static Dictionary<int, List<ProtoStar>> protoNodes;
        private static void Log(string msg, Text txt){
            Debug.Log(msg);
            txt.text = msg;
        }
        public async void onLoadingScreen(Scene scene, LoadSceneMode mode){
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= onLoadingScreen;
            if (Application.isEditor)
            {
                Debug.Log("Application.isEditor BUILD");
                var iterator = buildGameRoutine();
                iterator.MoveNext();
                while(iterator.Current != null){
                    await Task.Delay(25);
                    iterator.MoveNext();
                }
            }else{
                Debug.Log("Application.isNOTEditor BUILD");
                StartCoroutine(buildGameRoutine());
            }
        }

        public void buildGame(GameManager gameManager,Dictionary<int, List<ProtoStar>> protoNodes){
            SceneLoader.protoNodes = protoNodes;
            SceneLoader.gameManager = gameManager;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += onLoadingScreen;
            SceneLoader.LoadByIndex(1);
        }
        public static IEnumerator buildGameRoutine(){
            Debug.Log("loading screen loaded");
            var spritesStr = AssetSingleton.bundleNames.sprites;
            var prefabStr = AssetSingleton.bundleNames.prefabs;
            var bundles = new Dictionary<string, AssetBundle> () { { spritesStr,null  }, { prefabStr,null }};

            var textEl = GameObject.Find("Text").GetComponent<Text>();
            if(!textEl){
                Debug.LogError("startGame cant find text");
            }
            textEl.text = "loading";
            yield return "null";

            Log("loading bundles",textEl);
            if (!Application.isEditor){
                Log("loading bundles: "+spritesStr,textEl);
                yield return loadBundle(spritesStr,bundles);
                Log("loading bundles: "+prefabStr,textEl);
                yield return loadBundle(prefabStr,bundles);
            }else{
                Log("loading bundles: "+spritesStr,textEl);
                loadBundleSync(spritesStr,bundles);
                Log("loading bundles: "+prefabStr,textEl);
                loadBundleSync(prefabStr,bundles);
            }
            yield return "null";

            Log("loading assets ",textEl);
            SceneLoader.loadAssets(bundles);
            yield return new WaitForSeconds(.1f);

            Log("Making Factories",textEl);
            // gameManager.shipFactory = gameManager.gameObject.AddComponent<ShipFactory>();
            var creator = GameObject.Instantiate(gameManager.GameCreatorPrefab);
            gameManager.galaxyCreator = creator.GetComponentInChildren<GameGalaxyCreator>();
            creator.SetActive(true);
            var galHolder = GameObject.Find("Galaxy");
            galHolder.transform.SetParent(gameManager.transform);
            yield return new WaitForSeconds(.1f);

            Log("creating user faction",textEl);
            var faction = gameManager.factions.createFaction("my Faction");
            gameManager.user = new User(faction);
            yield return new WaitForSeconds(.1f);

            Log("converting protostars to starnodes",textEl);
            gameManager._starNodes = new StarNodeCollection(gameManager.galaxyCreator.hydrate(protoNodes));
            yield return new WaitForSeconds(.1f);

            Log("setting up UI",textEl);
            gameManager.mainUI.setManager(gameManager);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += gameManager.getSceneCanvas;
            yield return new WaitForSeconds(.1f);

            Log("loading galaxy scene",textEl);
            LoadByIndex(2);
            yield return new WaitForSeconds(.1f);
            Debug.Log("rendering galaxy");
            gameManager._starNodes.render(2);
            gameManager.mainUI.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }

        public static void onStarLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded=>onStarLoaded()=>renderStar(star): scene.name:" + scene.name + " selectedStar:" + gameManager.selectedStar);
            gameManager.selectedStar.render((int)util.Enums.sceneNames.StarSystemView);
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

        private static IEnumerator loadBundle (string name, Dictionary<string, AssetBundle> bundles) {
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
        private static AssetBundle loadBundleSync(string name, Dictionary<string, AssetBundle> bundles) {
            string path;
            path = Application.dataPath+"/" +"StreamingAssets/bundles/"+name;

            var bundle  = AssetBundle.LoadFromFile(path);
            if (bundle == null) {
                Debug.LogError ("Failed to load " + name + " !    assetPaths:" + path + "\n    Application.dataPath    " + Application.dataPath);
            }
            bundles[name] = bundle;
            return bundle;
        }
        public static void loadAssets (Dictionary<string, AssetBundle> bundles) {
            foreach (var bundle in bundles) {
                AssetSingleton.addBundle (bundle.Key, bundle.Value);
            }
        }
    }

}