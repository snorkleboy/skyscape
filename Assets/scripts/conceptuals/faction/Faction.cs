using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;
using Objects;
namespace Objects.Conceptuals{
	[System.Serializable]
	public class FactionModel
	{
		public FactionModel(){}
		public FactionModel(Faction faction){
			id=faction.id;
			name=faction.name;
		}
		public long id;
		public string name;
	}
	public class Faction :MonoBehaviour,ISaveAble<FactionModel>,IIded,IViewable
	{
		private iconInfo baseInfo;
		public long id;
		public FactionModel model{get{return new FactionModel(this);}}
		public long getId(){return id;}
		public Dictionary<string,Planet> ownedPlanets = new Dictionary<string,Planet>();
		public Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();
		public FleetFactory fleetFactory;
		public Fleet createFleet(Planet planet){
			var fleet = fleetFactory.makeFleet(this,planet.parentStar.value, planet.appearer.activeGO.transform.position + new Vector3(2,0,2));
			return fleet;
		}
		public Fleet createFleet(FleetModel model, StarNode star){
			var fleet = fleetFactory.makeFleet(this,star, model);
			return fleet;
		}
		public string factionName;
		public Sprite icon;

		public int money;

		public int updateId{get;}
		public void Awake(){
			icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[2];
			var starDetails = new iconInfo(ownedPlanets.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0]);
			var finDetails = new iconInfo(money.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[0]);
			var fleetDetails = new iconInfo(fleets.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[1]);
			baseInfo = new iconInfo(name,icon,this,new iconInfo[]{starDetails,finDetails,fleetDetails});
			fleetFactory = gameObject.AddComponent<FleetFactory>();
		}
		public iconInfo getIconableInfo(){
			var info = baseInfo;
			return info;
		}
		public GameObject renderUIView(Transform parent, clickViews callbacks){
			var info = getIconableInfo();
			return new GameObject();
		}
	}
}

