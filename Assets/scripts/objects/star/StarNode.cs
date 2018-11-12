using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class StarNode : IRenderable, IUIable
    {
        public int updateId{get;set;}
        public IRenderer renderHelper { get { return starRenderer; } }
        public HolderRenderer<StarNode> starRenderer;
        public Transform transform { get { return renderHelper.transform; } }
        public Vector3 position;
        public Sprite Icon;

        public string name;
        [SerializeField] private List<StarConnection> _connections;
        public StarNode(HolderRenderer<StarNode> renderer, Sprite Icon)
        {
            name = StarNames.names[UnityEngine.Random.Range(0,StarNames.names.Count-1)];
            this.Icon = Icon;
            this.starRenderer = renderer;
            renderer.scriptSingelton = this;
            connections = new List<StarConnection>();
        }
        public GameObject renderIcon(){
            var star = new GameObject("StarIcon");
            star.AddComponent<StarStub>().starnode = this;
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
            connections.Add(connection);
            starRenderer.addRenderables(connections.ToArray());
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


        public void render(int scene)
        {
            starRenderer.render(scene, position);
            if (scene == 3){
                starRenderer.transform.position = Vector3.zero;
            }else{
                starRenderer.transform.position = position;
            }
        }

    }

}
