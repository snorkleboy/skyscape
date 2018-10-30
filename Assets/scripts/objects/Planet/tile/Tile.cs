using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Tile: IContextable
    {
        [SerializeField]public Building building;
        [SerializeField]public Sprite sprite;
        public Tile(Sprite texture)
        {
            sprite = texture;
            this.building = new Building();
        }
        public Tile(Sprite sprite, Building building):this(sprite){
            this.building = building;
        }
        public GameObject renderContext(Transform parent, Action<IActOnable> a, Action<IViewable> b){
            Debug.Log("RENDER TILE CONTEXT");
            return new GameObject("Tile");
        }
    }


}
