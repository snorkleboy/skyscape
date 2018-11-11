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
        public List<ICreator<StarNode>> creatorStack = new List<ICreator<StarNode>>();
        public GameObject GameCreatorPrefab;
        public ShipFactory shipFactory;
        void Awake()
        {
            Debug.Log("game manager awake");
        }
        private int scene;
        private StarNode selectedStar;
        public async Task startgame(Dictionary<int, List<ProtoStar>> protoNodes)
        {
            instance = this;
            Debug.Log("Start Game Called, loading loading screen");
            loadByIndex(1);
            await Task.Delay(200);
            Debug.Log("loading screen loaded");
            await buildGame(protoNodes);
        }
        public async Task buildGame(Dictionary<int, List<ProtoStar>> protoNodes){
            Debug.Log("loading bundles");
            var bundles = SceneLoader.loadBundles();
            SceneLoader.loadAssets(bundles);
            
            Debug.Log("Making Factories");
            shipFactory = this.gameObject.AddComponent<ShipFactory>();
            var creator = GameObject.Instantiate(GameCreatorPrefab);
            var galCreator = creator.GetComponentInChildren<GameGalaxyCreator>();
            creator.SetActive(true);
            var galHolder = GameObject.Find("Galaxy");
            galHolder.transform.SetParent(transform);
            await Task.Delay(200);

            Debug.Log("converting protostars to starnodes");
            _starNodes = new StarNodeCollection(galCreator.hydrate(protoNodes));

            Debug.Log("creating user faction");
            var faction = new Faction("my Faction");
            factions.factions.Add(faction.name,faction);
            user = new User(faction);

            Debug.Log("setting up UI");
            mainUI.setManager(this);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += getSceneCanvas;

            Debug.Log("loading galaxy scene");
            loadByIndex(2);
            Debug.Log("rendering galaxy");
            _starNodes.render(2);
            mainUI.transform.gameObject.SetActive(true);
            Debug.Log("IN MAIN GAME");
        }
        private void getSceneCanvas(Scene scene, LoadSceneMode mode){
            Debug.Log("getting canvas  "+ scene.buildIndex);
            sceneCanvas = CanvasProvider.canvas;
            if (sceneCanvas == null){
                Debug.LogWarning("scene canvas not found. scene:"+ scene.buildIndex);
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
