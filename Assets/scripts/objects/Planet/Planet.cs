using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class Planet : IRenderable
    {
        public IRenderer renderHelper { get { return planetRenderer; } }
        private SingleSceneRenderer planetRenderer { get; set; }
        public Transform transform { get { return renderHelper.transform; } }
        private Vector3 _position;
        public Planet(SingleSceneRenderer renderer)
        {
            planetRenderer = renderer;
        }
        public void render(int scene)
        {
            planetRenderer.render(1);
        }
    }

}
