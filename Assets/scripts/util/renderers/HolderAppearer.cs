using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    [System.Serializable]
    public partial class HolderAppearer: BaseAppearable{
        public HolderAppearer(IAppearer appearer){
            this.thisAppearer = appearer;
        }
        public HolderAppearer(IAppearer appearer,IAppearable appearable):this(appearer){
            this.appearables.Add(appearable);
        }
        public HolderAppearer(IAppearer appearer,IAppearable[] appearables):this(appearer){
            this.appearables.AddRange(appearables);
        }     
    }

    public partial class HolderAppearer{
        private IAppearer thisAppearer;
        public override Transform attachementPoint{get{return thisAppearer.attachementPoint;}}
        public List<IAppearable> appearables = new List<IAppearable>();
        public void setAppearables(IAppearable[] renderables){
            this.appearables = new List<IAppearable>();
            addAppearables(renderables);
        }
        public void addAppearables(IAppearable[] renderablesIn)
        {
            this.appearables.AddRange(renderablesIn);
        }
        public void addAppearables(IAppearable renderable)
        {
            this.appearables.Add(renderable);
        }
    }
    public partial class HolderAppearer
    {
        public override bool appear(int scene){
            active = thisAppearer.appear(scene);

            foreach (var appearable in appearables)
            {
                appearable.appear(scene);
            }
            return active;
        }

        public override GameObject activeGO{
            get{return thisAppearer.activeGO;}
        }
        public override Vector3 getAppearPosition(int scene){
            return thisAppearer.getAppearPosition(scene);
        }
        public override void setAppearPosition(Vector3 position, int scene){
            thisAppearer.setAppearPosition(position,scene);
        }
        public override void destroy(){
            thisAppearer.destroy();
            foreach (var item in appearables)
            {
                item.appearer.destroy();
            }
        }
    }
}