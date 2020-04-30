using System.Collections;
using Newtonsoft.Json;
using Game.Core.State;

using Game.Core.Entities.Interfaces;
namespace Game.Core.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	[System.Serializable]
	public class Faction :IHasStateObject
	{
		public FactionState state{get;set;}
		public IHasID stateObject{get{return state;}set{state = (FactionState)value;}}
		public long getId() { return state.id; }
		////public IconInfo baseInfo;
		//private Sprite fleetSprite;
		//private Sprite finDetailsSprite;
		//private Sprite starDetailsSprite;
		public virtual bool isUsers
        {
            get { return true; }
        }

		public void init(FactionState state){
			this.state = state;
			//fleetSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[1];
			//finDetailsSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"ui")[0];
			//starDetailsSprite = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
			//fleetFactory = gameObject.AddComponent<FleetFactory>();
		}
		//public FleetFactory fleetFactory;
		//public Fleet createFleet(Planet planet){
			//var fleet = fleetFactory.makeFleet(this,planet.state.positionState.starAt, planet.state.positionState.position + new Vector3(2,0,2));
			//return fleet;
		//}
		// public Fleet createFleet(FleetModel model, StarNode star){
		// 	var fleet = fleetFactory.makeFleet(this,star, model);
		// 	return fleet;
		// }


	}
}

