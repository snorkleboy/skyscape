using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [System.Serializable]
    public class HolderRenderer<type> : PerSceneRenderer<type>
    {
        public Dictionary<Guid, IRenderable> renderables = new Dictionary<Guid, IRenderable>();
        public void setRenderables(IRenderable[] renderables){
            this.renderables = new Dictionary<Guid, IRenderable>();
            addRenderables(renderables);
        }
        public void addRenderables(IRenderable[] renderablesIn)
        {
            foreach (var renderable in renderablesIn)
            {
                this.renderables[renderable.renderHelper.uid] = renderable;
            }
        }
        public void addRenderables(IRenderable renderable)
        {
            this.renderables[renderable.renderHelper.uid] = renderable;
        }
        public HolderRenderer(GameObject[] sceneToPrefab, Transform parent, type script) : base(sceneToPrefab, parent,script)
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
                    if (renderable.renderHelper.parent == null){
                        renderable.renderHelper.parent = activeGO.transform;
                    }
                    
                    renderable.render(scene);
                }
            }
            return true;
        }
        public override void destroy(){
            base.destroy();
            foreach (var item in renderables)
            {
                item.Value.renderHelper.destroy();
            }
        }
    }
}
