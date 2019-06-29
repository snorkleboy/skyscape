using System;
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
		public Dictionary<long,Reference<Planet>> ownedPlanets = new Dictionary<long,Reference<Planet>>();
		public Dictionary<long,Reference<Fleet>> fleets = new Dictionary<long, Reference<Fleet>>();
		public long id;
		public string factionName;
		[NonSerializedAttribute]public Sprite icon;
		public int money;
	}
	[JsonObject(MemberSerialization.OptIn)]
	[System.Serializable]
	public class Faction :MonoBehaviour,IUIable,IIded,IViewable,ISaveable<FactionState>
	{
		public FactionState state{get;set;}
		public object stateObject{get{return state;}set{state = (FactionState)value;}}
		public long getId(){return state.id;}
		public IconInfo baseInfo;
		private Sprite fleetSprite;
		private Sprite finDetailsSprite;
		private Sprite starDetailsSprite;

		public void init(FactionState state){
			this.state = state;
			fleetSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[1];
			finDetailsSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[0];
			starDetailsSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
			fleetFactory = gameObject.AddComponent<FleetFactory>();
		}
		public FleetFactory fleetFactory;
		public Fleet createFleet(Planet planet){
			var fleet = fleetFactory.makeFleet(this,planet.state.positionState.starAt, planet.appearer.state.position + new Vector3(2,0,2));
			return fleet;
		}
		// public Fleet createFleet(FleetModel model, StarNode star){
		// 	var fleet = fleetFactory.makeFleet(this,star, model);
		// 	return fleet;
		// }


		public int updateId{get;}
		public void updateIconInfo(){
			var starDetails = new IconInfo(state.ownedPlanets.Count.ToString(),starDetailsSprite);
			var finDetails = new IconInfo(state.money.ToString(),finDetailsSprite);
			var fleetDetails = new IconInfo(state.fleets.Count.ToString(),fleetSprite);
			baseInfo = new IconInfo(state.factionName,state.icon,this,new IconInfo[]{starDetails,finDetails,fleetDetails});
		}
		public IconInfo getIconableInfo(){
			updateIconInfo();
			var info = baseInfo;
			return info;
		}
		public GameObject renderUIView(Transform parent, clickViews callbacks){
			var info = getIconableInfo();
			return new GameObject();
		}
	}
}

