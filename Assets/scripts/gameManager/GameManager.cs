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
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public User user;
        public StarNodeCollection _starNodes;
        public FactionManager factions = new FactionManager();
        public Transform sceneCanvas;
        public MainUI mainUI;
        public List<ICreator<StarNode>> creatorStack = new List<ICreator<StarNode>>();
        public GameObject GameCreatorPrefab;
        public ShipFactory shipFactory;
        public GameGalaxyCreator galaxyCreator;
        public SceneLoader sceneLoader;
        void Awake()
        {
            Debug.Log("game manager awake");
        }
        private int scene;
        public StarNode selectedStar;
        public void startgame(Dictionary<int, List<ProtoStar>> protoNodes)
        {
            instance = this;
            Debug.Log("Start Game Called, loading loading screen");
            sceneLoader.buildGame(this,protoNodes);
        }
        public void getSceneCanvas(Scene scene, LoadSceneMode mode){
            sceneCanvas = CanvasProvider.canvas;
            if (sceneCanvas == null){
                Debug.LogWarning("scene canvas not found. scene:"+ scene.buildIndex);
            }
        }
        public void loadStarSystem(StarStub starstub)
        {
            loadStarSystem(starstub.starnode);
        }
        public void loadStarSystem(StarNode star){
            SceneLoader.loadStarSystem(star);
        }
        public void renderGalaxyView(){
            SceneLoader.renderGalaxyView();
        }
    }

}
