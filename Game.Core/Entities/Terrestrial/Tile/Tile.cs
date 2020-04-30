using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Game.Core.State;
using Game.Core.Entities.Interfaces;
namespace Game.Core.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
    public partial class Tile: IHasStateObject
    {
        public long getId(){
            return state.id;
        }
        public IHasID stateObject{get{return state;}set{state = (TileState)value;}}

        public TileState state{get;set;}

        // public Tile(Sprite texture, Building building, TileModel model)
        // {
        //     title = "tile";
        //     sprite = texture;
        //     this.tilePosition = model.position;
        //     this.building = building;
        // }
        public Tile(TileState state){
            this.state = state;
        }

    }

    

}
