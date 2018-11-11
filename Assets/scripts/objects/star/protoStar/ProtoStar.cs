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
    public class ProtoStar:IRenderable
    {
        public IRenderer renderHelper { get { return starRenderer; } }
        public HolderRenderer<ProtoStar> starRenderer;
        public Transform transform { get { return renderHelper.transform; } }
        public Vector3 position;

        [SerializeField] private List<ProtoStarConnection> _connections;
        public ProtoStar(HolderRenderer<ProtoStar> renderer)
        {
            this.starRenderer = renderer;
            connections = new List<ProtoStarConnection>();
        }
        public List<ProtoStarConnection> connections
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
        public void addConnection(ProtoStarConnection connection)
        {
            connections.Add(connection);
            starRenderer.addRenderables(connections.ToArray());
        }
        public void render(int scene)
        {
            starRenderer.render(scene, position);
            starRenderer.transform.position = position;
        }

    }
    class ProtoStarRenderer : HolderRenderer<ProtoStar>{
        public ProtoStarRenderer(GameObject[] sceneToPrefab, Transform holder) : base(sceneToPrefab, holder)
        {
        }
        public override void applyScript(GameObject activeGO, ProtoStar scriptSingelton){

        }
    }

}
