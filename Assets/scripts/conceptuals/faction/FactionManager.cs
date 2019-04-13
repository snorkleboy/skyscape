using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
namespace Objects.Conceptuals{
    public class FactionManager : MonoBehaviour
    {
        public Dictionary<string,Faction> factions = new Dictionary<string,Faction>();
        public Faction userFaction = null;
        private Dictionary<Planet,Faction> planetToFactions = new Dictionary<Planet, Faction>();
        public Faction setUserFaction(string name){
            userFaction = createFaction(name);
            return userFaction;
        }
        public Faction createFaction(FactionState state){
            return createFaction(state.factionName,state.id);
        }
        public Faction createFaction(string name, long id = -1){
            var factionHolder = new GameObject(name);
            factionHolder.transform.SetParent(this.transform);
            var faction = factionHolder.AddComponent<Faction>();
            var factionState = new FactionState(){
                factionName = name
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