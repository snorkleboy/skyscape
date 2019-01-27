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
        public TileManager makeTileManager(TileModel[] tileModels, int tileWidth){
            var width = tileWidth;
            var height = width;
            Tile[] tiles = new Tile[tileModels.Length];
            for (int i =0; i<tiles.Length;i++){
                tiles[i] = makeTile(tileModels[i]);
            }
            return new TileManager(width, height, tiles);
        }
        public TileManager makeTileManager(){
            var width = (int)Random.Range(5,15);
            var height = width;
            Tile[] tiles = new Tile[width*height];
            for (int i =0; i<tiles.Length;i++){
                tiles[i] = makeTile(i);
            }
            return new TileManager(width, height, tiles);
        }
        public Tile makeTile(TileModel model){
            Building building = null;
            if (model.building != null){
                Pop[] pops = new Pop[0];
                if(model.building.pops != null && model.building.pops.Length > 0){
                    pops = new Pop[model.building.pops.Length];
                    var count = 0;
                    foreach(var popModel in model.building.pops){
                        var pop =  new Pop(popSprites[0],popModel);
                        pop.id = GameManager.idMaker.insertObject(pop,popModel.id);
                        pops[count++] = pop;
                    }
                }
                building = new Building(buildingSprites[0],pops,model.building);
                building.id = GameManager.idMaker.insertObject(building,model.building.id);
            }
            var tileSprite = tileSprites[Random.Range(0,tileSprites.Length)];
            var tile = new Tile(tileSprite,building,model);
            tile.id = GameManager.idMaker.insertObject(tile, model.id);
            return tile;
        }
        public Tile makeTile(int tileNum){
            var shouldMakeBuilding = Random.Range(0,10)>3;
            Tile tile;
            if (shouldMakeBuilding){
                var pops = makePops(Random.Range(1,10));
                var building = makeBuilding(pops);
                tile= new Tile(tileSprites[Random.Range(0,tileSprites.Length)], building,tileNum);
            }else{
                tile =  new Tile(tileSprites[Random.Range(0,tileSprites.Length)],tileNum);
            }
            tile.id = GameManager.idMaker.newId(tile);
            return tile;
        }

        public Pop[] makePops(int num){
            Pop[] pops = new Pop[num];
            for(var i = 0; i<num;i++){
                pops[i] = new Pop(popSprites[0]);
                pops[i].id = GameManager.idMaker.newId(pops[i]);
            }
            return pops;
        }
        public Building makeBuilding(Pop[] pops){
           var building =  new Building(buildingSprites[0],pops);
           building.id = GameManager.idMaker.newId(building);
           return building;
        }
    }
}
