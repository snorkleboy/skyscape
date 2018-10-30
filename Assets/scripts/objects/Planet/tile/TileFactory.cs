using System.Collections.Generic;
using UnityEngine;
namespace Objects.Galaxy.Holdable
{
    public class TileFactory: MonoBehaviour
    {
        public Sprite[] sprites;
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
            var sprite = sprites[(int)Random.Range(0,sprites.Length)];
            var makeBuilding = Random.Range(0,10)>3;
            if (makeBuilding){
                Pop[] pops = new Pop[Random.Range(1,10)];
                for(var i = 0; i<pops.Length;i++){
                    pops[i] = new Pop();
                }
                var building = new Building(pops);
                return new Tile(sprite, building);
            }else{
                return new Tile(sprite);
            }
        }
    }
}
