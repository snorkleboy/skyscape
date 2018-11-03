using System.Collections.Generic;
using UnityEngine;
namespace Objects.Galaxy.Holdable
{
    public class TileFactory: MonoBehaviour
    {
        public Sprite[] sprites;
        public Sprite[] popSprites;
        public Sprite[] buildingSprites;
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
                return new Tile(sprites[Random.Range(0,sprites.Length)], building,tileNum);
            }else{
                return new Tile(sprites[Random.Range(0,sprites.Length)],tileNum);
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
