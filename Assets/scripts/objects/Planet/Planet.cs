﻿using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy;
using UnityEngine.UI;
using UI;
namespace Objects.Galaxy
{

    [System.Serializable]
    public class Planet :IViewable,IContextable, IRenderable
    {
        public int updateId{get;}
        public IRenderer renderHelper { get { return planetRenderer; } }
        public string title{get;}
        private SingleSceneRenderer<Planet> planetRenderer { get; set; }
        public Transform transform { get { return renderHelper.transform; } }
        public Sprite planetSprite;
        [SerializeField]public Vector3 position;
        [SerializeField]public TileManager tileManager;
        public Planet(SingleSceneRenderer<Planet> renderer,Sprite planetSprite)
        {
            title = "planet";
            this.planetSprite = planetSprite;
            renderer.scriptSingelton = this;
            planetRenderer = renderer;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            Debug.Log("planet render UIVIEW");
            callbacks.contextViewCallback(this);
            return tileManager.renderUIView(parent,callbacks);
        }
        public GameObject renderContext(Transform parent, clickViews callbacks){
            Debug.Log("RENDER PLANET CONTEXT");
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
        public void render(int scene)
        {
            if (planetRenderer.render(scene)){
                planetRenderer.transform.Translate(position);
            }
        }
    }

}
