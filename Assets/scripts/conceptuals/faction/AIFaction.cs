using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects;
using Newtonsoft.Json;
namespace Objects.Conceptuals{
    public class AIFactionState : FactionState{

    }
	public class AIFaction :Faction{
        private bool testBool = false;
        private void Awake(){
            Debug.Log("AIFaction  Awake");
            StartCoroutine(ai());
        }
        public Dictionary<long,bool> madeFleetForPlanet = new Dictionary<long,bool>();
        private IEnumerator ai(){
            Debug.Log("start AI routine");
            while(true){
                yield return new WaitForSeconds(10);
                Debug.Log(this.state.factionName + " " + "ownedPlanets length:   " + this.state.ownedPlanets.Values.ToArray().Length);
                foreach(var pair in this.state.ownedPlanets){
                    if(!madeFleetForPlanet.ContainsKey(pair.Value.id)){
                        madeFleetForPlanet[pair.Value.id] = true;
                        var fleet = createFleet(pair.Value.value);
                        fleet.appearer.appear(3);
                        Debug.Log(this.state.factionName +" created fleet : " + fleet);
                    }
                }
            }
        }
        

    }

}