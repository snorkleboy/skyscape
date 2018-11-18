using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Loaders;
namespace Objects.Galaxy
{
    public class Ship : IRenderable
    {
        public IRenderer renderHelper {get;set;}
        public void render(int context){
            renderHelper.render(context);
        }
        public Sprite icon;
        public void Init(SingleSceneRenderer<Ship> renderer){
            renderHelper = renderer;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
        }
    }

}