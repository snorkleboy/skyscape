using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Planet : IRenderable
    {
        public IRenderer renderHelper { get { return planetRenderer; } }
        private SingleSceneRenderer<Planet> planetRenderer { get; set; }
        public Transform transform { get { return renderHelper.transform; } }
        public Vector3 position;
        public Planet(SingleSceneRenderer<Planet> renderer)
        {
            renderer.scriptSingelton = this;
            planetRenderer = renderer;
        }
        public void render(int scene)
        {
            planetRenderer.render(1);
            planetRenderer.transform.Translate(position);
        }
    }

}
