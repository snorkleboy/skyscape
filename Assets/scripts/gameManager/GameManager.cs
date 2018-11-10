using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using GalaxyCreators;
using System.IO;
using Objects.Conceptuals;
using Loaders;
namespace Objects
{
    public class User{
        public Faction faction;
        public User(Faction faction){
            this.faction = faction;
        }
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public User user;
        private StarNodeCollection _starNodes;
        public FactionManager factions = new FactionManager();
        public Transform sceneCanvas;
        public MainUI mainUI;
        public List<CreatorWare> creatorStack = new List<CreatorWare>();

        public ShipFactory shipFactory = new ShipFactory();
        void Awake()
        {
            Debug.Log("game manager awake");
            Debug.Log("loading bundles");
            var bundles = SceneLoader.loadBundles();
            SceneLoader.loadAssets(bundles);

            instance = this;
        }
        private int scene;
        private StarNode selectedStar;
        public async Task startgame(Dictionary<int, List<StarNode>> starNodes)
        {
            Debug.Log("Start Game Called");
            _starNodes = new StarNodeCollection(starNodes);
            await loadStartGame();
        }
        private void getSceneCanvas(Scene scene, LoadSceneMode mode){
            Debug.Log("getting canvas  "+ scene.buildIndex);
            sceneCanvas = CanvasProvider.canvas;
            if (sceneCanvas == null){
                Debug.LogWarning("scene canvas not found. scene:"+ scene.buildIndex);
            }
        }
        private async Task loadStartGame(){
            Debug.Log("destroying protogalaxy");
            _starNodes.destroy();
            loadByIndex(1);
            Debug.Log("loading screen loaded");
            Debug.Log("Hydrating Galaxy");
            hydrateProtoGalaxy();
            Debug.Log("creating user faction");
            var faction = new Faction("my Faction");
            Debug.Log(faction.name);
            factions.factions.Add(faction.name,faction);
            user = new User(faction);
            Debug.Log("setting ui");
            mainUI.setManager(this);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += getSceneCanvas;
            Debug.Log("loading galaxy scene");
            loadByIndex(2);
            Debug.Log("rendering galaxy");
            _starNodes.render(2);
            mainUI.transform.gameObject.SetActive(true);
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
            loadStarSystem(starstub.starnode);
        }
        public void loadStarSystem(StarNode star){
            Debug.Log("loading star system");
            Debug.Log("destroy old");
            _starNodes.destroy();
            Debug.Log("load scene");
            loadByIndex(3);
            selectedStar = star;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += onStarLoaded;
        }
        public void renderGalaxyView(){
            SceneLoader.LoadByIndex(2);
            _starNodes.render(2);

        }
        private void loadByIndex(int i){
             SceneLoader.LoadByIndex(i);
        }


    }

}
