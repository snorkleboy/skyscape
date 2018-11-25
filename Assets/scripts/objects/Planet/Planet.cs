﻿using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy;
using UnityEngine.UI;
using UI;
namespace Objects.Galaxy
{

    [System.Serializable]
    public partial class Planet :MonoBehaviour, IViewable,IContextable,IActOnable, IRenderable
    {
        public int updateId{get;}
        public IRenderer renderHelper { get { return planetRenderer; } }
        public string title{get;set;}
        private SingleSceneRenderer<Planet> planetRenderer { get; set; }
        public Sprite planetSprite;
        [SerializeField]public Vector3 position;
        [SerializeField]public TileManager tileManager;
        public void Init(SingleSceneRenderer<Planet> renderer,Sprite planetSprite )
        {
            title = Names.planetNames.getName();
            this.planetSprite = planetSprite;
            gameObject.name = title;
            renderer.scriptSingelton = this;
            planetRenderer = renderer;
            GameManager.instance.factions.registerPlanetToFaction(this,GameManager.instance.user.faction);
        }

        public void render(int scene)
        {
            if (planetRenderer.render(scene)){
                planetRenderer.transform.position = (position);
                planetRenderer.transform.gameObject.name = "planet representation";
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
                fleet.render(3);
                // fleet.transform.position = renderHelper.transform.position + new Vector3(2,0,2);
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
        public iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = title;
            info.icon = planetSprite;
            return info;
        }
    }

}
