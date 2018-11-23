using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public partial class Fleet:MonoBehaviour,IUIable,IViewable,IRenderable,ISelectable,IMoveable
    {

        public IRenderer renderHelper{get;set;} 
        public void render(int scene){
            renderHelper.render(scene);
            renderHelper.transform.position = transform.position;
        }
        public Mover mover{get;set;}
        public ShipManager ships = new ShipManager();
        public Pop admiral;
        public string name;
        public Sprite icon;

        public List<inputAction> controls;
        public void Awake(){
            controls = new List<inputAction>()
                {
                    new inputAction(
                        ()=>
                        {
                            var down = Input.GetMouseButtonDown(1);
                            return down;
                        },
                        ()=>
                        {
                            Debug.Log("set position " + this);
                            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                            {
                                var newvec = hit.point;
                                newvec.y = 0;
                                mover.setTarget(newvec);
                                util.Line.DrawTempLine(transform.position,newvec,Color.green,4);
                            }else{
                                Debug.Log("no hit");
                            }

                        }
                    ),
                };
        }
        public void Init(string name, Sprite icon, HolderRenderer<Fleet> renderHelper){
            this.name = name;
            this.icon = icon;
            this.renderHelper = renderHelper;
            var a = getIconableInfo();
            mover = gameObject.AddComponent<Mover>();
            Debug.Log("ICON INFO   "+a.name+" | " + a.icon+" | ");
        }
        public void Init(string name,Sprite icon,HolderRenderer<Fleet> renderHelper, List<Ship> ships){
            Init(name,icon,renderHelper);
            this.ships.addShips(ships);
        }
        public InputController getInputController(GameObject parent){
            var controller = parent.AddComponent<InputController>();
            controller.Init(controls ,this.gameObject);
            Debug.Log("controller  " + controller);
            return controller;
        }
    }
    public partial class Fleet {
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

}