using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
using Loaders;

namespace Objects
{
    public partial class Fleet:MoveAbleGameObject,IViewable,ISelectable
    {
        public override IMover mover{get{return moverHelper;}}
        private Mover moverHelper;
        private ShipManager ships = new ShipManager();
        public void Init(string name, Sprite icon, HolderRenderer<Fleet> renderHelper){
            this.name = name;
            this.icon = icon;
            this.holderRenderer = renderHelper;
            var a = getIconableInfo();
            moverHelper = gameObject.AddComponent<Mover>();
            Debug.Log("ICON INFO   "+a.name+" | " + a.icon+" | ");
        }
        public Fleet addShips(Ship ship){
            this.ships.addShips(ship);
            holderRenderer.addRenderables(ship);
            return this;
        }
        public Fleet addShips(List<Ship> ships){
            this.ships.addShips(ships) ;
            holderRenderer.addRenderables(ships.ToArray());
            return this;
        }
        public InputController getInputController(GameObject parent){
            var controller = parent.AddComponent<InputController>();
            controller.Init(controls ,this.gameObject);
            Debug.Log("controller  " + controller);
            return controller;
        }
    }
    // rendering
    public partial class Fleet{
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
        public override IRenderer renderHelper{get{return holderRenderer;}} 
        private HolderRenderer<Fleet> holderRenderer;
        public override void render(int scene){
            renderHelper.render(scene);
            renderHelper.transform.position = transform.position;
            var count = 0;
            foreach (var renderable in holderRenderer.renderables){
                renderable.Value.renderHelper.transform.position = transform.position + new Vector3(1 + count++,0,0);
            }
        }
    }
    //ui
    public partial class Fleet {
        public override iconInfo getIconableInfo(){
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