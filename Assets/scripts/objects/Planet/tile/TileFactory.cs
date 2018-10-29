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
        public Tile makeTile(int i){
            return new Tile(sprites[(int)Random.Range(0,sprites.Length)]);
        }

    }
}
