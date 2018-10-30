using System.Collections.Generic;
using UnityEngine;
using System;
namespace UI{
    public interface IIconable
    {
        GameObject renderIcon();
    }
    public interface IViewable : IUIable{
        GameObject renderUIView(Transform parent,Action<IContextable> renderContextCallBack);
    }
    public interface IActOnable{
        GameObject renderActionView(Transform parent);
    }
    public interface IContextable{
        GameObject renderContext(Transform parent,Action<IActOnable> renderActionViewCallBack,Action<IViewable> renderViewCallBack);
    }

    public interface IUIComplex{
        IViewable[] viewManagers{get;}
    }

}