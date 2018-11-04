using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using GalaxyCreators;
using System.IO;
using Loaders;
namespace Objects
{
    public static class GameManagerSingleton
    {
        public static GameManager gameManager{get;set;}

    }
    public class GameManager : MonoBehaviour
    {
        private StarNodeCollection _starNodes;
        [SerializeField]
        public List<CreatorWare> creatorStack = new List<CreatorWare>();
        void Awake()
        {
            Debug.Log("game manager awake");
            GameManagerSingleton.gameManager = this;
            Debug.Log("loading bundles");
            var bundles = SceneLoader.loadBundles();
            Debug.Log("loading assets");
            SceneLoader.loadAssets(bundles);
        }
        private int scene;
        private StarNode selectedStar;
        public async Task startgame(Dictionary<int, List<StarNode>> starNodes)
        {
            Debug.Log("Start Game Called");
            _starNodes = new StarNodeCollection(starNodes);
            loadStartGame();
        }
        private async Task loadStartGame(){
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            Debug.Log("loading screen loaded");
            await Task.Delay(1000);
            Debug.Log("destroying protogalaxy");
            _starNodes.destroy();

            Debug.Log("Hydrating Galaxy");
            hydrateProtoGalaxy();
            Debug.Log("rendering galaxy");
            _starNodes.render(2);
            Debug.Log("loading galaxy scene");
            SceneLoader.LoadByIndex(2);
            Debug.Log("IN MAIN GAME");
        }
        public void hydrateProtoGalaxy(){
            foreach (CreatorWare creator in creatorStack)
            {
                creator.actOn(_starNodes._starNodes);
            }
        }
        void onStarLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);
            Debug.Log("render star");
            selectedStar.render((int)util.Enums.sceneNames.StarSystemView);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= onStarLoaded;
        }
        public void loadStarSystem(StarStub starstub)
        {
            Debug.Log("loading star system");
            var star = starstub.starnode;
            Debug.Log("destroy old");

            _starNodes.destroy();
            Debug.Log("load scene");
            SceneLoader.LoadByIndex(3);
            selectedStar = star;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += onStarLoaded;
        }

    }

}
