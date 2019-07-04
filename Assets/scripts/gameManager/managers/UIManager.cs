using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using GalaxyCreators;
using System.IO;
using Objects.Conceptuals;
using Loaders;
using UI;
namespace Objects
{
    public class UIManager : MonoBehaviour
    {
        public Transform sceneCanvas = null;
        public Transform GMcanvas;
        public MainUI mainUI;
        GameManager gameManager;

        public InputController cameraController = null;
        public InputController objectinputController = null;
        public DragBox dragSelect;

        private void Update() {
            if(objectinputController != null){
                objectinputController.checkAction();
            }
            if(cameraController != null){
                cameraController.checkAction();
            }

        }
        public ClickViewDetailPanel getDetailView()
        {

            var view =  GMcanvas.GetComponentInChildren<ClickViewDetailPanel>(true);
            if (view == null)
            {
                Debug.LogWarning("planet on click could not find a PlanetClickView");
            }
            return view;
        }
        public void setGameManager(GameManager manager){
            gameManager = manager;
        }
        public void getSceneCanvas(Scene scene, LoadSceneMode mode){
            Debug.Log("UIMANAGER:getSceneCanvas:"+ scene.buildIndex);
            sceneCanvas = CanvasProvider.canvas;
            if (sceneCanvas == null){
                Debug.LogWarning("scene canvas not found. scene:"+ scene.buildIndex);
            }else{
                dragSelect = null;
                if (dragSelect = sceneCanvas.GetComponentInChildren<DragBox>()){
                    dragSelect.onMouseUp = getObjectsInBox;
                    dragSelect.onStartDrag = ()=>{
                        objectinputController = null;
                    };
                }
            }
            cameraController = Camera.main.gameObject.GetComponent<InputController>();
        }
        public void getObjectsInBox(Vector3 start, Vector3 end){
            Debug.Log("dragselect ONMOUSEUP vectors:  " + start + " " + end);
            var bounds = util.UIExt.GetViewportBounds(Camera.main,start,end);
            var fleets = util.UIExt.getObjectsInBox<Fleet>(bounds);
            Debug.Log("fleets.length :" + fleets.Count);
            if (fleets.Count == 0 ){
                var planets = util.UIExt.getObjectsInBox<Planet>(bounds);
                Debug.Log("planets.length:" + planets.Count);
            }else{
                objectinputController = fleets[0].controller;
            }
        }
        
    }
}