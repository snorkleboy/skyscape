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
        public IRenderer renderHelper {get;}
        public void render(int context){
            renderHelper.render(context);
        }
        public Sprite icon;
        public Ship(SingleSceneRenderer<Ship> renderer)
        {
            renderHelper = renderer;
            icon = AssetSingleton.getBundledDirectory<Sprite>(AssetSingleton.bundleNames.sprites,"star")[0];
        }
    }

    public class ShipRenderer : SingleSceneRenderer<Ship>
    {
        public ShipRenderer(GameObject prefab) : base(prefab,3)
        {
        }
        public override void applyScript(GameObject go, Ship script)
        {
            go.GetComponent<ShipStub>().ship = script;
        }

    }
}