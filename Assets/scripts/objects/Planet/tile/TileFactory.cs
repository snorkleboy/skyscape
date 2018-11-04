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
            Debug.Log("SPRITES:"+tileSprites.Length + " " + popSprites.Length + " " + buildingSprites.Length);
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
        public Tile makeTile(int tileNum){
            var shouldMakeBuilding = Random.Range(0,10)>3;
            if (shouldMakeBuilding){
                var pops = makePops(Random.Range(1,10));
                var building = makeBuilding(pops);
                return new Tile(tileSprites[Random.Range(0,tileSprites.Length)], building,tileNum);
            }else{
                return new Tile(tileSprites[Random.Range(0,tileSprites.Length)],tileNum);
            }
        }
        public Pop[] makePops(int num){
            Pop[] pops = new Pop[num];
            for(var i = 0; i<num;i++){
                pops[i] = new Pop(popSprites[0]);
            }
            return pops;
        }
        public Building makeBuilding(Pop[] pops){
           return new Building(buildingSprites[0],pops);
        }
    }
}
