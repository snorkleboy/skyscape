using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public class Fleet:MonoBehaviour,IViewable,IRenderable
    {
        public IRenderer renderHelper{get;set;} 
        public void render(int scene){

        }
        public ShipManager ships = new ShipManager();
        public Pop admiral;
        public string name;

        public Sprite icon;
        public void Init(string name, Sprite icon, HolderRenderer<Fleet> renderHelper){
            this.name = name;
            this.icon = icon;
            this.renderHelper = renderHelper;
        }
        public void Init(string name,Sprite icon,HolderRenderer<Fleet> renderHelper, List<Ship> ships){
            Init(name,icon,renderHelper);
            this.ships.addShips(ships);
        }
        
        public iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = name;
            info.icon = icon;
            return info;
        }
        public GameObject renderUIView(Transform parent, clickViews callbacks){
            return new GameObject();
        }
    }

    public class ShipManager{
        public ShipManager(){

        }
        public ShipManager(List<Ship> ships){
            addShips(ships);
        }
        public List<Ship> ships = new List<Ship>();
        public ShipManager addShips(Ship ship){
            this.ships.Add(ship);
            return this;
        }
        public ShipManager addShips(List<Ship> ships ){
            this.ships.AddRange(ships);
            return this;
        }
    }
}