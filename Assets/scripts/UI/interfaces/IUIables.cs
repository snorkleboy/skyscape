using System.Collections.Generic;
using UnityEngine;
using System;
namespace UI{
    public interface IUpdateable{
        int updateId{get;}
    }
    public interface IUIable : IUpdateable{
        GameObject renderIcon();
        string title{get;}
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

    public interface IUIComplex{
        IViewable[] viewManagers{get;}
    }

}