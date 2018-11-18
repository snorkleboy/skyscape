using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
namespace Objects.Galaxy
{

    
    public partial class StarNode : MonoBehaviour, IRenderable, IUIable
    {
        public void Init(HolderRenderer<StarNode> renderer, Sprite Icon)
        {
            name = Names.starNames.getName();
            gameObject.name = name;
            this.Icon = Icon;
            this.starRenderer = renderer;
        }
        public void render(int scene)
        {
            starRenderer.render(scene, transform.position);
            if (scene == 3){
                starRenderer.transform.position = Vector3.zero;
            }
        }

    }
    [System.Serializable]
    public partial class StarNode{
        public int updateId{get;set;}
        public IRenderer renderHelper { get { return starRenderer; } }
        [SerializeField] public HolderRenderer<StarNode> starRenderer;
        public Sprite Icon;
        public string name;
        private List<StarConnection> _connections = new List<StarConnection>();
        public List<StarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
                starRenderer.addRenderables(_connections.ToArray());
            }
        }
        public void addConnection(StarConnection connection)
        {
            if (!(connection.nodes[1] == this)){
                connections.Add(connection);
                starRenderer.addRenderables(connection);
            }            
        }
        [SerializeField] private Planet[] _planets;
        public Planet[] planets {
            get
            {
                return _planets;
            }
            set
            {
                _planets = value;
                starRenderer.addRenderables(_planets);
            }
        }
    }
    public partial class StarNode{
        public GameObject renderIcon(){
            var star = new GameObject("StarIcon");
            var image = star.AddComponent<Image>();
            image.sprite = Icon;
            return star;
        }
        public iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = name;
            info.icon = Icon;
            var details = new iconInfo[2];
            var detail = new iconInfo();
            detail.name = planets.Length.ToString();
            var bundle = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites];
            var asset = bundle.LoadAsset<Sprite>("43");
            detail.icon = asset;
            details[0] = detail;

            var otherDetail = new iconInfo();
            var popNum = 0;
            foreach(var planet in planets){
               foreach(var tile in  planet.tileManager.tiles){
                   if (tile.building != null){
                       if (tile.building.pops != null){
                           popNum += tile.building.pops.Count;
                       }
                   }
               }
            }
            otherDetail.name = popNum.ToString();
            otherDetail.icon =  AssetSingleton.bundles[AssetSingleton.bundleNames.sprites].LoadAsset<Sprite>("69");
            details[1] = otherDetail;

            info.details = details;
            return info;
        }
    }

}
