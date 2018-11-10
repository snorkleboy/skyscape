using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects.Conceptuals{
	public class Faction :IViewable{
		private iconInfo baseInfo;
		public Dictionary<string,StarNode> ownedStars = new Dictionary<string,StarNode>();
		public Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();
		public string name;
		public Sprite icon;

		public Pop leader;


		public int money;

		//temp
		public Sprite tempIcon;

		public int updateId{get;}

		public Faction(string name){
			this.name = name;
			icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[2];
			var starDetails = new iconInfo(ownedStars.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0]);
			var finDetails = new iconInfo(money.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[0]);
			var fleetDetails = new iconInfo(fleets.Count.ToString(),AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[1]);
			baseInfo = new iconInfo(name,icon,this,new iconInfo[]{starDetails,finDetails,fleetDetails});
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

