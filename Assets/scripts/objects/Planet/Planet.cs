using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy;
using UI;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Planet :IViewable, IRenderable
    {
        public int updateId{get;}
        public IRenderer renderHelper { get { return planetRenderer; } }
        private SingleSceneRenderer<Planet> planetRenderer { get; set; }
        public Transform transform { get { return renderHelper.transform; } }
        [SerializeField]public Vector3 position;
        [SerializeField]public TileManager tileManager;
        public Planet(SingleSceneRenderer<Planet> renderer)
        {
            renderer.scriptSingelton = this;
            planetRenderer = renderer;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            Debug.Log("planet render UIVIEW");
            return tileManager.renderUIView(parent,callbacks);
        }
        public void render(int scene)
        {
            if (planetRenderer.render(scene)){
                planetRenderer.transform.Translate(position);
            }
        }
    }

}
