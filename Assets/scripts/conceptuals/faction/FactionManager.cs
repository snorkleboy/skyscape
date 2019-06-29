using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Loaders;
using UI;
namespace Objects.Conceptuals{
    public class FactionManager : MonoBehaviour
    {
        public Sprite[] factionSprites;
        private int factionsI = 0;
        private bool gotSprites = false;
        private void Awake() {
        }
        public Dictionary<string,Faction> factions = new Dictionary<string,Faction>();
        public Faction[] factionsDebug{get{return factions.Values.ToArray();}}
        public Faction userFaction = null;
        private Dictionary<Planet,Faction> planetToFactions = new Dictionary<Planet, Faction>();
        public Faction setUserFaction(string name){
            userFaction = createFaction(name);
            return userFaction;
        }
        public Faction createFaction(FactionState state){
            if(!gotSprites){
                gotSprites=true;
                factionSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"faction");
            }
            if(state.GetType() == typeof(AIFactionState)){
                return createAIFaction(state.factionName,state.id);
            }else{
                return createFaction(state.factionName,state.id);
            }
        }
        public AIFaction createAIFaction(string name, long id = -1){
            if(!gotSprites){
                gotSprites=true;
                factionSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"faction");
            }
            var factionHolder = new GameObject(name);
            factionHolder.transform.SetParent(this.transform);
            var faction = factionHolder.AddComponent<AIFaction>();
            var factionState = new AIFactionState(){
                factionName = name,
                icon = factionSprites[(factionsI++) % factionSprites.Length]
            };
            faction.init(factionState);
            factions[faction.name] = faction;
            if(id == -1){
                var maker = GameManager.idMaker;
                faction.state.id = GameManager.idMaker.newId(faction);
            }else{
                faction.state.id = GameManager.idMaker.insertObject(faction,id);
            }
            return faction;
        }
        public Faction createFaction(string name, long id = -1 ){
            if(!gotSprites){
                gotSprites=true;
                factionSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"faction");
            }
            var factionHolder = new GameObject(name);
            factionHolder.transform.SetParent(this.transform);
            var faction = factionHolder.AddComponent<Faction>();
            var factionState = new FactionState(){
                factionName = name,
                icon = factionSprites[(factionsI++) % factionSprites.Length]
            };
            faction.init(factionState);
            factions[faction.name] = faction;
            if(id == -1){
                var maker = GameManager.idMaker;
                faction.state.id = GameManager.idMaker.newId(faction);
            }else{
                faction.state.id = GameManager.idMaker.insertObject(faction,id);
            }
            return faction;
        }
        public Faction registerPlanetToFaction(Planet planet, Faction faction){
            planetToFactions[planet] = faction;
            if (factions[faction.name] != faction){
                Debug.LogWarning("unknown faction " + faction);
            }
            faction.state.ownedPlanets.Add(planet.state.id,planet);
            return faction;
        }
        public Faction GetFaction(string faction){
            return factions[faction];
        }
        public Faction GetFaction(Planet planet){
            return planetToFactions[planet];
        }
 
        
    }
}