using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class TileFactory: MonoBehaviour
    {
        private Sprite[] tileSprites;
        private Sprite[] popSprites;
        private Sprite[] buildingSprites;
        public void Start(){
            popSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"pop");
            buildingSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"building");
            tileSprites = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"tile");
        }
        // public Tileable makeTileManager(TileModel[] tileModels, int tileWidth){
        //     Tile[] tiles = new Tile[tileModels.Length];
        //     for (int i =0; i<tiles.Length;i++){
        //         tiles[i] = makeTile(tileModels[i]);
        //     }
        //     var state = new TileableState(){
        //         width = tileWidth,
        //         height = tileWidth,
        //         tiles = tiles
        //     };
        //     return new Tileable(state);
        // }
        public Tileable makeTileManager(){
            var width = (int)Random.Range(5,15);
            Tile[] tiles = new Tile[width*width];
            for (int i =0; i<tiles.Length;i++){
                tiles[i] = makeTile(i);
            }
            var state = new TileableState(){
                width = width,
                height = width,
                tiles = tiles.referenceAll().ToArray()
            };
            return new Tileable(state);
        }
        // public Tile makeTile(TileModel model){
        //     Building building = null;
        //     if (model.building != null){
        //         Pop[] pops = new Pop[0];
        //         if(model.building.pops != null && model.building.pops.Length > 0){
        //             pops = new Pop[model.building.pops.Length];
        //             var count = 0;
        //             foreach(var popModel in model.building.pops){
        //                 var pop =  new Pop(popSprites[0],popModel);
        //                 pop.id = GameManager.idMaker.insertObject(pop,popModel.id);
        //                 pops[count++] = pop;
        //             }
        //         }
        //         building = new Building(buildingSprites[0],pops,model.building);
        //         building.id = GameManager.idMaker.insertObject(building,model.building.id);
        //     }
        //     var tileSprite = tileSprites[Random.Range(0,tileSprites.Length)];
        //     var tile = new Tile(tileSprite,building,model);
        //     tile.id = GameManager.idMaker.insertObject(tile, model.id);
        //     return tile;
        // }
        public Tile makeTile(int tileNum){
            var shouldMakeBuilding = Random.Range(0,10)>3;
            Tile tile;
            var state =new TileState(){
                sprite=tileSprites[Random.Range(0,tileSprites.Length)],
                tilePosition = tileNum,
                named = new State.NamedState("tile")
            };
            if (shouldMakeBuilding){
                var pops = makePops(Random.Range(1,10));
                var building = makeBuilding(pops);
                state.building = (Reference<Building>)building;
                tile= new Tile(state);
            }else{
                tile =  new Tile(state);
            }
            tile.state.id = GameManager.idMaker.newId(tile);
            return tile;
        }

        public Pop[] makePops(int num){
            Pop[] pops = new Pop[num];
            for(var i = 0; i<num;i++){
                var popState = new PopState(){
                    sprite = popSprites[0],
                };
                pops[i] = new Pop(popState);
                pops[i].state.id = GameManager.idMaker.newId(pops[i]);
            }
            return pops;
        }
        public Building makeBuilding(Pop[] pops){
            var buildingState = new BuildingState(){
                sprite = buildingSprites[0],
                pops = pops.referenceAll()
            };
           var building =  new Building(buildingState);
           building.state.id = GameManager.idMaker.newId(building);
           return building;
        }
    }
}
