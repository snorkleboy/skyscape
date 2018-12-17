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

    public class Planetable: ScriptableObject{
        [SerializeField]private Planet[] _planets;
        public Planet[] planets {
            get
            {
                return _planets;
            }
            set
            {
                _planets = value;
            }
        }
    }
    
    public class Connectable : ScriptableObject{
        [SerializeField] List<StarConnection> _connections = new List<StarConnection>();
        public List<StarConnection> connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
            }
        }
        public void addConnection(StarConnection connection)
        {
            connections.Add(connection);
        }

    }
    public struct StarNodeModel{
        public SerializableVector3 position;
        FactoryStamp stamp;
        public long id;
        public StarConnectionModel[] starConnections;
        public PlanetModel[] planets;

        public StarNodeModel(StarNode node){
            position = node.transform.position;
            stamp = node.stamp;
            id = node.id;

            var numConn = node.connectable.connections.Count;
            starConnections = new StarConnectionModel[numConn];
            for(var i =0;i<numConn;i++)
            {
                starConnections[i] = node.connectable.connections[i].model;
            }

            var planetNum = node.planetable.planets.Length;
            planets = new PlanetModel[planetNum];
            for(var i =0; i<planetNum;i++){
                planets[i] = node.planetable.planets[i].model;
            }
        }
    }
    
    //init
    public partial class StarNode : MonoBehaviour, IRenderable, IUIable, ISaveAble<StarNodeModel>
    {
        public long id;
        public StarNodeModel model{get{return new StarNodeModel(this);}}
        public FactoryStamp stamp;

        public void Init(HolderRenderer<StarNode> renderer, Sprite Icon)
        {
            name = Names.starNames.getName();
            gameObject.name = name;
            this.Icon = Icon;
            this.starRenderer = renderer;
            this.connectable = ScriptableObject.CreateInstance<Connectable>();
            this.planetable = ScriptableObject.CreateInstance<Planetable>();
        }
        public void render(int scene)
        {
            starRenderer.render(scene, transform.position);
            if (scene == 3){
                starRenderer.transform.position = Vector3.zero;
            }
        }

    }
    //attributes
    [System.Serializable]
    public partial class StarNode{
        public IRenderer renderHelper { get { return starRenderer; } }
        public HolderRenderer<StarNode> starRenderer;
        public Sprite Icon;
        [SerializeField]public Connectable connectable;
        public void addConnection(StarConnection connection)
        {
            if (connection.nodes[0] == this){
                connectable.addConnection(connection);
                starRenderer.addRenderables(connection);
            }            
        }
        [SerializeField]public Planetable planetable;
        public void setPlanets(Planet[] planets) {
            planetable.planets = planets;
            starRenderer.addRenderables(planets);
        }

    }
    //ui
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
            detail.name = planetable.planets.Length.ToString();
            var bundle = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites];
            var asset = bundle.LoadAsset<Sprite>("43");
            detail.icon = asset;
            details[0] = detail;

            var otherDetail = new iconInfo();
            var popNum = 0;
            foreach(var planet in planetable.planets){
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
