using System.Collections.Generic;
using UnityEngine;
using System;
namespace UI{
    public interface IIconable
    {
        GameObject renderIcon(clickViews viewCallBacks);
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