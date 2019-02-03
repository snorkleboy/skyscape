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
using util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Objects
{
    public class GameManagerModel{
        public GameManagerModel(){}
        public GameManagerModel(GameManager gm){
            idMaker = GameManager.idMaker.count;
            _starNodes = gm._starNodes.model;
        }
        public long idMaker;
        public StarNodeCollectionModel _starNodes;

    }

    public class GameManager : MonoBehaviour, ISaveAble<GameManagerModel>
    {
        public static GameManager instance;

        [SerializeField]public GameObject GameCreatorPrefab;
        [SerializeField]public GameGalaxyCreator galaxyCreator;
        [SerializeField]public SceneLoader sceneLoader;
        public GameManagerModel model{get{return new GameManagerModel(this);}}


        public ObjectTable objectTable;
        public static UniqueIdMaker idMaker;

        [SerializeField]public User user;
        [SerializeField]public StarNodeCollection _starNodes;
        [SerializeField]public FactionManager factions;
        [SerializeField]public UIManager UIManager;
 
        void Awake()
        {
            Debug.Log("game manager awake");
            instance = this;
            factions = GetComponentInChildren<FactionManager>();
            if (!factions){
                Debug.LogWarning("game manger couldnt find FactionManager");
            }
            UIManager = GetComponentInChildren<UIManager>();
            if (!UIManager){
                Debug.LogWarning("game manger couldnt find UIManager");
            }
        }
        [SerializeField]private int scene;
        [SerializeField]public StarNode selectedStar;
        public void startgame(Dictionary<int, List<ProtoStar>> protoNodes)
        {
            scrub();
            Debug.Log("Start Game Called, loading loading screen");
            sceneLoader.buildGame(this,buildFromProtoStars(protoNodes));
        }
        
        private IEnumerator buildFromProtoStars(Dictionary<int,List<ProtoStar>> protoNodes){
            yield return null;
            Debug.Log("HERE");
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(0,objectTable);

            var faction = instance.factions.createFaction("my faction");
            instance.user = new User(faction);
            var collection = new Dictionary<int, List<StarNode>>();
            yield return instance.galaxyCreator.hydrate(protoNodes,collection);
            instance._starNodes = new StarNodeCollection(collection);
        }
        public void startgame(SavedGame savedGame){
            scrub();     
            sceneLoader.buildGame(this,buildGameFromSave(savedGame));
        } 
        private IEnumerator buildGameFromSave(SavedGame savedGame){
            yield return null;
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(savedGame.loadedModel.idMaker,objectTable);

            var faction = instance.factions.createFaction("my Faction");
            instance.user = new User(faction);
            var collection = new Dictionary<int, List<StarNode>>();
            yield return instance.galaxyCreator.hydrate(savedGame.loadedModel._starNodes.starNodes,collection);
            instance._starNodes = new StarNodeCollection(collection);
        }
        public void Save() {
            SavedGameManager.Save(this.model);
        }
        private void scrub(){
            if(_starNodes != null){
                _starNodes.destroy();
                foreach(Transform child in GameManager.instance.gameObject.transform){
                    if(child.gameObject.name == "Galaxy"){
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
        public void loadStarSystem(StarNode star){
            SceneLoader.loadStarSystem(star);
        }
        public void renderGalaxyView(){
            SceneLoader.renderGalaxyView();
        }
    }

}
