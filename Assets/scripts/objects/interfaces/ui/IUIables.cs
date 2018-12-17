using System.Collections.Generic;
using UnityEngine;
using System;
namespace UI{

    
    public struct iconInfo{
        public iconInfo(string name,Sprite icon,IUIable source = null, iconInfo[] details=null){
            this.source = source;
            this.name = name;
            this.icon = icon;
            this.details = details;
        }
        public IUIable source;
        public string name;
        public Sprite icon;
        public iconInfo[] details;

    }

    public interface IUIable {
        iconInfo getIconableInfo();

    }

    public interface IIconable : IUIable
    {
        GameObject renderIcon(clickViews viewCallBacks);
        List<GameObject> renderInfo(clickViews viewCallBacks);

    }
    public interface IViewable : IUIable{
        GameObject renderUIView(Transform parent,clickViews viewCallBacks);
    }
    public interface IActOnable{
        GameObject renderActionView(Transform parent,clickViews viewCallBacks);
    }
    public interface IContextable{
        GameObject renderContext(Transform parent,clickViews viewCallBacks);
    }


}