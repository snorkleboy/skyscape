using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using UnityEditor.SceneManagement;
using GalaxyCreators;
namespace Objects
{
    public class GameManager : MonoBehaviour
    {
        private StarNodeCollection _starNodes;
        [SerializeField]
        public List<CreatorWare> creatorStack = new List<CreatorWare>();

        public loadScene sceneLoader;
        void Awake()
        {
            Debug.Log("game manager awake");
        }
        private int scene;

        private StarNode selectedStar;
        public async Task startgame(Dictionary<int, List<StarNode>> starNodes)
        {
            Debug.Log("Start Game Called");
            _starNodes = new StarNodeCollection(starNodes);
            Debug.Log("destroying");
            _starNodes.destroy();
            Debug.Log("loading 1");
            SceneManager.LoadSceneAsync(1);
            Debug.Log("onLoadWaitingScreen");
            loadStartGame();
        }
        private void loadStartGame(){
            Debug.Log("IN MAIN LOADING SCENE, putting more stuff on`");
            foreach (CreatorWare creator in creatorStack)
            {
                creator.actOn(_starNodes._starNodes);
            }
            Debug.Log("rendering");
            _starNodes.render(2);
            Debug.Log("loading 2");
            sceneLoader.LoadByIndex(2);
            Debug.Log("IN MAIN GAME");
        }
        void onStarLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);
            Debug.Log("render star");
            selectedStar.render((int)util.Enums.sceneNames.StarSystemView);
            SceneManager.sceneLoaded -= onStarLoaded;
        }
        public void loadStarSystem(StarStub starstub)
        {
            Debug.Log("loading star system");
            var star = starstub.starnode;
            Debug.Log("destroy old");

            _starNodes.destroy();
            Debug.Log("load scene");
            sceneLoader.LoadByIndex(3);
            selectedStar = star;
            SceneManager.sceneLoaded += onStarLoaded;
        }

    }

}
