﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using GalaxyCreators;
using Objects.Conceptuals;
using Loaders;
using UnityEngine.EventSystems;
namespace Objects
{
    public class SaveGameModel {
        public SaveGameModel() { }
        public SaveGameModel(GameManager gm) {
            idMaker = GameManager.idMaker.count;
            starNodes = gm._starNodes.starNodeRef;
            objectTable = gm.objectTable.objects.toStateTable();
            factions = gm.factions.factions.Values.referenceAll();
        }
        public long idMaker;
        public Dictionary<long, object> objectTable;
        public List<Reference<Faction>> factions;
        public Dictionary<int, List<Reference<StarNode>>> starNodes;


    }

    public partial class GameManager : MonoBehaviour
    {
        public static UIManager uiManager
        {
            get { return GameManager.instance.UIManager; }
        }

        public static GameManager instance;
        [SerializeField]public GameObject GameCreatorPrefab;
        [SerializeField]public GameGalaxyCreator galaxyCreator;
        [SerializeField]public SceneLoader sceneLoader;
        public SaveGameModel model{get{return new SaveGameModel(this);}}

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
            objectTable = new ObjectTable();
        }
        [SerializeField]private int scene;
        [SerializeField]public StarNode selectedStar;

        public void loadStarSystem(StarNode star){
            SceneLoader.loadStarSystem(star);
        }
        public void renderGalaxyView(){
            SceneLoader.renderGalaxyView();
        }
        public FactoryProvider factories;
        public void startgame(Dictionary<int, List<ProtoStar>> protoNodes)
        {
            // scrub();
            Debug.Log("Start Game Called, loading loading screen");
            sceneLoader.buildGame(this,buildFromProtoStars(protoNodes));
        }
        public void startgame(SavedGame savedGame){
            // scrub();     
            sceneLoader.buildGame(this,buildGameFromSave(savedGame));
        } 
        private IEnumerator buildFromProtoStars(Dictionary<int,List<ProtoStar>> protoNodes){
            yield return null;
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(0,objectTable);
            var collection = new Dictionary<int, List<StarNode>>();
            var userFaction = factions.setUserFaction("my faction");
            user = new User(userFaction);
            var AIFaction = factions.createAIFaction("Ai1");
            var AI2Faction = factions.createAIFaction("Ai2");

            factories.fleetFactory = new Dictionary<Faction,FleetFactory>(){
                {userFaction,userFaction.fleetFactory},
                {AIFaction,AIFaction.fleetFactory},
                {AI2Faction,AI2Faction.fleetFactory},
            };
            AI2Faction.startAI();
            AIFaction.startAI();
            yield return instance.galaxyCreator.hydrate(protoNodes,collection);
            instance._starNodes = new StarNodeCollection(collection);

        }

        private IEnumerator buildGameFromSave(SavedGame savedGame){
            yield return null;
            objectTable = new ObjectTable();
            idMaker = new UniqueIdMaker(savedGame.loadedModel.idMaker,objectTable);
            foreach(var factionRef in savedGame.loadedModel.factions){
                factions.createFaction((FactionState)savedGame.loadedModel.objectTable[factionRef.id]);
            }
            instance.user = new User(factions.userFaction);
            var collection = new Dictionary<int,List<StarNode>>();
            yield return galaxyCreator.hydrate(savedGame,collection);
            instance._starNodes = new StarNodeCollection(collection);
        }
        public void Save(string name) {
            SavedGameManager.Save(this.model,name);
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

