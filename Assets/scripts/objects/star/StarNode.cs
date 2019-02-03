using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
using Loaders;
using Objects.Conceptuals;
using UnityEditor;
using System.Linq;
namespace Objects.Galaxy
{


    public class StarNodeModel{
        public SerializableVector3 position;
        FactoryStamp stamp;
        public long id;
        public string name;
        public StarConnectionModel[] starConnections;
        public PlanetModel[] planets;
        public FleetModel[] fleets;
        public StarNodeModel(){}
        public StarNodeModel(StarNode node){
            position = node.transform.position;
            stamp = node.stamp;
            id = node.id;
            name=node.name;
            var numConn = node.connectable.connections.Count;
            starConnections = new StarConnectionModel[numConn];
            for(var i =0;i<numConn;i++)
            {
                starConnections[i] = node.connectable.connections[i].model;
            }

            var planetNum = node.planetable.planets.Count;
            planets = new PlanetModel[planetNum];
            for(var i =0; i<planetNum;i++){
                planets[i] = node.planetable.planets[i].value.model;
            }
        }
    }
    
    //init
    public partial class StarNode : MonoBehaviour, IAppearable, IUIable, ISaveAble<StarNodeModel>,IIded
    {
        public long id;
        public long getId(){
            return id;
        }
        public Transform getChildrenTransform(){return this.transform.Find("children");}
        public Transform getRepresentationTransform(){return appearer.appearTransform;}
        public StarNodeModel model{get{return new StarNodeModel(this);}}
        public FactoryStamp stamp;

        public void Init(LinkedAppearer renderer, Sprite Icon)
        {
            name = Names.starNames.getName();
            gameObject.name = name;
            this.Icon = Icon;
            this.starAppearer = renderer;
            this.connectable = ScriptableObject.CreateInstance<Connectable>();
            this.planetable = ScriptableObject.CreateInstance<Spaceable>();
        }
        public void appear(int scene)
        {
            starAppearer.appear(scene );//,transform.position
            if (scene == 3){
                starAppearer.activeGO.transform.position = Vector3.zero;
            }
        }
    }
    //attributes
    [System.Serializable]
    public partial class StarNode{
        public List<Reference<Fleet>> fleets = new List<Reference<Fleet>>();
        public IAppearer appearer { get { return starAppearer; } }
        public LinkedAppearer starAppearer;
        public Sprite Icon;
        [SerializeField]public Connectable connectable;
        public void addConnection(StarConnection connection)
        {
            bool alreadyAdded = connectable.connections.Any(existingConnection=>(
                existingConnection.nodes.Any(node=>node.getId() == existingConnection.nodes[0].getId()) && 
                existingConnection.nodes.Any(node=>node.getId() == existingConnection.nodes[1].getId())
            ));
            if (!alreadyAdded){
                connectable.addConnection(connection);
                addAppearable(connection);
            }
            
        }
        [SerializeField]public Spaceable planetable;
        public void setPlanets(Reference<Planet>[] planets) {
            planetable.planets = planets.ToList();
            addAppearable(planets.getAllReferenced());
        }
        public void fleetEnter(Fleet fleet){
            enterStar(fleet);
            fleets.Add(new Reference<Fleet>(fleet));
        }

        public void enterStar(IAppearable appearable){
            appearable.appearer.appearTransform.SetParent(getChildrenTransform());
            addAppearable(appearable);
        }
        public void addAppearable(IAppearable appearable){
            starAppearer.addAppearables(appearable);
            
        }
        public void addAppearable(IEnumerable<IAppearable> appearables){
            starAppearer.addAppearables(appearables);
        }
        public void exitStar(IAppearable appearable){
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
            detail.name = planetable.planets.Count.ToString();
            var bundle = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites];
            var asset = bundle.LoadAsset<Sprite>("43");
            detail.icon = asset;
            details[0] = detail;

            var otherDetail = new iconInfo();
            var popNum = 0;
            foreach(var planet in planetable.planets){
               foreach(var tile in  planet.value.tileManager.tiles){
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
