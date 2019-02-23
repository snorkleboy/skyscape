﻿using System.Collections;
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
            var list = new List<FactionModel>();
            foreach(var faction in gm.factions.factions.Values){
                list.Add(faction.model);
            }
            factions = list.ToArray();
        }
        public long idMaker;
        public StarNodeCollectionModel _starNodes;
        public FactionModel[] factions;

    }

    public partial class GameManager : MonoBehaviour, ISaveAble<GameManagerModel>
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
        public class testclass: IIded{
            public testclass(){
            }
            public string thing = "hi";
            public long id;
            public long getId(){return id;}
        }
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
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(1232,objectTable);
            var tc = new testclass();
            tc.id = idMaker.newId(tc);
            var thing = new Reference<testclass>(tc);
            Debug.Log(thing.getId());
            var ser = JsonConvert.SerializeObject(thing);
            Debug.Log(ser);
            var deser = JsonConvert.DeserializeObject<Reference<testclass>>(ser);
            Debug.Log(deser.getId());
            Debug.Log(deser.value.getId());

        }
        [SerializeField]private int scene;
        [SerializeField]public StarNode selectedStar;

        public void loadStarSystem(StarNode star){
            SceneLoader.loadStarSystem(star);
        }
        public void renderGalaxyView(){
            SceneLoader.renderGalaxyView();
        }
    }
    public partial class GameManager{
        public void startgame(Dictionary<int, List<ProtoStar>> protoNodes)
        {
            scrub();
            Debug.Log("Start Game Called, loading loading screen");
            sceneLoader.buildGame(this,buildFromProtoStars(protoNodes));
        }
        public void startgame(SavedGame savedGame){
            scrub();     
            sceneLoader.buildGame(this,buildGameFromSave(savedGame));
        } 
        private IEnumerator buildFromProtoStars(Dictionary<int,List<ProtoStar>> protoNodes){
            yield return null;
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(0,objectTable);

            var faction = instance.factions.setUserFaction("my faction");
            instance.user = new User(faction);
            var collection = new Dictionary<int, List<StarNode>>();
            yield return instance.galaxyCreator.hydrate(protoNodes,collection);
            instance._starNodes = new StarNodeCollection(collection);
        }

        private IEnumerator buildGameFromSave(SavedGame savedGame){
            yield return null;
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(savedGame.loadedModel.idMaker,objectTable);
            factions.createFactions(savedGame.loadedModel.factions);
            instance.user = new User(factions.userFaction);
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
    }
}

