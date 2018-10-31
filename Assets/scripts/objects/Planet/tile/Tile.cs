using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Tile: IContextable, IIconable
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
        public GameObject renderContext(Transform parent,clickViews callbacks){
            Debug.Log("RENDER TILE CONTEXT");
            building.renderIcon(parent,callbacks);
            return new GameObject("Tile");
        }
        public GameObject renderIcon(Transform parent,clickViews callbacks){
            var tile = new GameObject("Tile");
            tile.transform.SetParent(parent);
            tile.AddComponent<TileStub>().tile = this;
            var image = tile.AddComponent<Image>();
            image.sprite = sprite;

            var button = tile.AddComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(()=>{Debug.Log("clicked" + this);callbacks.contextViewCallback(this);});
            return tile;
        }
    }


}
