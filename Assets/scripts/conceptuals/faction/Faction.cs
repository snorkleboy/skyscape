using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects;
using Newtonsoft.Json;
namespace Objects.Conceptuals{
	[System.Serializable]
	public class FactionState{
		public Dictionary<string,Planet> ownedPlanets = new Dictionary<string,Planet>();
		public Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();
		public IconInfo baseInfo;
		public long id;
		public string factionName;
		public Sprite icon;
		public int money;
	}
	[JsonObject(MemberSerialization.OptIn)]

	public class Faction :MonoBehaviour,IIded,IViewable,ISaveable<FactionState>
	{
		public FactionState state{get;set;}
		public object stateObject{get{return state;}set{state = (FactionState)value;}}
		public long getId(){return state.id;}
		public void init(FactionState state){
			this.state = state;
			// state.icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[2];
			// var starDetails = new IconInfo(state.ownedPlanets.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0]);
			// var finDetails = new IconInfo(state.money.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[0]);
			// var fleetDetails = new IconInfo(state.fleets.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[1]);
			// state.baseInfo = new IconInfo(name,state.icon,this,new IconInfo[]{starDetails,finDetails,fleetDetails});
			fleetFactory = gameObject.AddComponent<FleetFactory>();
		}
		public FleetFactory fleetFactory;
		public Fleet createFleet(Planet planet){
			var fleet = fleetFactory.makeFleet(this,planet.state.positionState.starAt, planet.appearer.state.position + new Vector3(2,0,2));
			return fleet;
		}
		public Fleet createFleet(FleetModel model, StarNode star){
			var fleet = fleetFactory.makeFleet(this,star, model);
			return fleet;
		}


		public int updateId{get;}
	
		public IconInfo getIconableInfo(){
			var info = state.baseInfo;
			return info;
		}
		public GameObject renderUIView(Transform parent, clickViews callbacks){
			var info = getIconableInfo();
			return new GameObject();
		}
	}
}

