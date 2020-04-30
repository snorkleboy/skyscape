using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.State;
namespace Game.Core.Entities
{
    public class AIFactionState : FactionState{

    }
	public class AIFaction :Faction{
        public override bool isUsers
        {
            get { return false; }
        }
        private void Awake(){
            Debug.Log("AIFaction  Awake");
        }
        public void startAI(){
            //StartCoroutine(ai());
        }
        public Dictionary<long,bool> madeFleetForPlanet = new Dictionary<long,bool>();
        private IEnumerator ai(){
            Debug.Log("start AI routine");
            while(true){
                yield return new WaitForSeconds(5);
                foreach(var pair in this.state.ownedPlanets){
                    if(!madeFleetForPlanet.ContainsKey(pair.Value.id)){
                        madeFleetForPlanet[pair.Value.id] = true;
                        var planet = pair.Value.value;
                        //var fleet = createFleet(planet);
                        //if(GameManager.instance.selectedStar != null && planet.state.positionState.starAt.id == GameManager.instance.selectedStar.state.id){
                            //fleet.appearer.appear(3);
                        //}
                        var offset = 25;
                        var points = new Vector3[]{
                            planet.state.positionState.position+new Vector3(offset,0,0),
                            planet.state.positionState.position+new Vector3(0,0,offset),
                            planet.state.positionState.position+new Vector3(-offset,0,0),
                            planet.state.positionState.position+new Vector3(0,0,-offset)
                        };
                        //fleet.setStateAction(fleet.patrol(points));
                    }
                }
            }
        }
        

    }

}