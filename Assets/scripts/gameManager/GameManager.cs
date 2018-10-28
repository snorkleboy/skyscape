using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

using Objects.Galaxy;
namespace Objects
{
    public class GameManager : MonoBehaviour
    {
        public GalaxyCreators.galaxyCreator gameCreator;
        private StarNodeCollection _starNodes;
        public loadScene sceneLoader;
        void Awake()
        {
            Debug.Log("game manager awake");
        }
        private int scene;
        public void startgame(Dictionary<int, List<StarNode>> starNodes)
        {
            _starNodes = new StarNodeCollection(starNodes);
            Debug.Log("loading 1");
            sceneLoader.LoadByIndex(1);
            Debug.Log("IN MAIN LOADING SCENE");
            _starNodes.destroy();
            _starNodes.render(2);

            Debug.Log("loading 2");
            sceneLoader.LoadByIndex(2);
            Debug.Log("IN MAIN GAME");
            var count = 0;


        }
        public void loadStarSystem(StarNode star)
        {
            Debug.Log("loading star system");
            sceneLoader.LoadByIndex(3);
            _starNodes.deactive();
            star.render((int)util.Enums.sceneNames.StarSystemView);
        }

    }

}
