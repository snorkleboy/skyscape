using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    public abstract class HolderRenderer<type> : PerSceneRenderer<type>
    {
        Dictionary<Guid, IRenderable> renderables = new Dictionary<Guid, IRenderable>();
        private Vector3 position;
        public void addRenderables(IRenderable[] renderablesIn)
        {
            foreach (var renderable in renderablesIn)
            {
                this.renderables[renderable.renderHelper.uid] = renderable;
            }
        }
        public HolderRenderer(GameObject[] sceneToPrefab, Transform parent) : base(sceneToPrefab, parent)
        {
        }
        public bool render(int scene, Vector3 position)
        {
            this.position = position;
            return render(scene);
        }

        public override bool render(int scene)
        {
            if(base.render(scene))
            {
                activeGO.transform.position = position;
                foreach (var renderable in renderables.Values)
                {
                    renderable.renderHelper.parent = activeGO.transform;
                    renderable.render(scene);
                }
            }
            return true;
        }
    }
}
