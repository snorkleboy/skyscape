using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy;
using UnityEngine.UI;
using UI;
using Objects.Conceptuals;
namespace Objects.Galaxy
{
    public class PlanetModel{
        public string name;
        public TileModel[] tiles;
        public int tileWidth;
        public long id;
        public SerializableVector3 position;
        public long factionId;
        public PlanetModel(){}
        public PlanetModel(Planet planet){
            tileWidth = planet.tileManager.width;
            name = planet.name;
            id = planet.id;
            position = planet.position;
            var length = planet.tileManager.tiles.Length;
            tiles = new TileModel[length];
            for(var i =0; i<length;i++){
                tiles[i] = planet.tileManager.tiles[i].model;
            }
            factionId = planet.owningFaction.id;
        }

    }
    [System.Serializable]
    public partial class Planet :MonoBehaviour, IViewable,IContextable,IActOnable, IAppearable, ISaveAble<PlanetModel>, IIded
    {
        public PlanetModel model{get{return new PlanetModel(this);}}
        public long id;
        public long getId(){
            return id;
        }
        public IAppearer appearer { get { return planetRenderer; } }
        public string title;
        public Reference<StarNode> parentStar;
        [SerializeField]private SingleSceneAppearer planetRenderer { get; set; }
        public Sprite planetSprite;
        public Vector3 position;
        public TileManager tileManager;
        public Faction owningFaction;
        public void Init(SingleSceneAppearer renderer,Sprite planetSprite,Reference<StarNode> star,PlanetModel model = null)
        {
            if(model !=null){
                title = model.name;
                owningFaction = model.factionId.dereference<Faction>();
            }else{
                title = Names.planetNames.getName();
                owningFaction = GameManager.instance.user.faction;
                GameManager.instance.factions.registerPlanetToFaction(this,owningFaction);
            }
            gameObject.name = title;
            parentStar = star;
            this.planetSprite = planetSprite;
            planetRenderer = renderer;

        }

        public void appear(int scene)
        {
            if (planetRenderer.appear(scene)){
                planetRenderer.activeGO.name = "planet representation";
            }
        }
    }
    public partial class Planet{
        public GameObject renderActionView(Transform parent, clickViews callbacks){
            var holder = new GameObject("planet action view");
            var layout = holder.AddComponent<VerticalLayoutGroup>();
            holder.SetParent(parent,false);
            layout.childControlHeight = false;
		    layout.childControlWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;

            var makeShipButton = new GameObject("make Fleet");
            var button = makeShipButton.AddComponent<Button>();
            button.onClick.AddListener(()=>{
                var fleet = GameManager.instance.factions.GetFaction(this).createFleet(this);
                Debug.Log("fleet created: " + fleet);
            });
            var text = makeShipButton.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.text = "make fleet";
            makeShipButton.SetParent(holder,false);
            return holder;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            callbacks.contextViewCallback(this);
            callbacks.actionViewCallBack(this);
            Debug.Log("RENDER PLANET UI VIEW");
            return tileManager.renderUIView(parent,callbacks);
        }
        public GameObject renderContext(Transform parent, clickViews callbacks){
            var holder =  new GameObject("PLANET Context");
            holder.transform.SetParent(parent, false);
            var text = new GameObject("planet info");
            var textComp = text.AddComponent<Text>();
            textComp.text = "number of tiles:" + tileManager.tiles.Length;
            textComp.fontSize = 16;
            textComp.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.transform.SetParent(holder.transform,false);
            return holder;
        }
        public IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = title;
            info.icon = planetSprite;
            return info;
        }
    }

}
