using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
namespace Objects.Conceptuals{
    public class FactionManager : MonoBehaviour
    {
        private Dictionary<string,Faction> factions = new Dictionary<string,Faction>();
        private Dictionary<Planet,Faction> planetToFactions = new Dictionary<Planet, Faction>();
        public Faction createFaction(string name){
            var factionHolder = new GameObject(name);
            factionHolder.transform.SetParent(this.transform);
            var faction = factionHolder.AddComponent<Faction>();
            faction.factionName = name;
            factions[faction.name] = faction;
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