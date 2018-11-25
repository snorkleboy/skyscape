using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects.Conceptuals{
	[System.Serializable]
	public class Faction :MonoBehaviour,IViewable{
		private iconInfo baseInfo;
		public Dictionary<string,Planet> ownedPlanets = new Dictionary<string,Planet>();
		public Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();
		public FleetFactory fleetFactory;
		public Fleet createFleet(Planet planet){
			var fleet = fleetFactory.makeFleet(this,planet.transform.parent, planet.transform.position + new Vector3(2,0,2));
			fleet.fleetPosition = planet.renderHelper.transform.position + new Vector3(2,0,2);
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

