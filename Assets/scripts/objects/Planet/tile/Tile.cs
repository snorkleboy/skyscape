using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Tile
    {
        [SerializeField]public Building building;
        [SerializeField]public Sprite sprite;
        public Tile(Sprite texture)
        {
            sprite = texture;
        }
    }


}
