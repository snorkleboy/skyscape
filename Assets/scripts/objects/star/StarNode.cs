using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;
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
        [SerializeField] private List<StarConnection> _connections;
        public StarNode(HolderRenderer<StarNode> renderer, Sprite Icon)
        {
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
            info.name = position.ToString();
            info.icon = Icon;
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
