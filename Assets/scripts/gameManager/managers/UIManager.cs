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

        public InputController inputController;
        public void setGameManager(GameManager manager){
            gameManager = manager;
        }
        public void getSceneCanvas(Scene scene, LoadSceneMode mode){
            Debug.Log("getSceneCanvas:"+ scene.buildIndex);
            sceneCanvas = CanvasProvider.canvas;
            if (sceneCanvas == null){
                Debug.LogWarning("scene canvas not found. scene:"+ scene.buildIndex);
            }else{
                DragBox dragSelect;
                if (dragSelect = sceneCanvas.GetComponentInChildren<DragBox>()){
                    dragSelect.onMouseUp = getObjectsInBox;
                    dragSelect.onStartDrag = ()=>{
                        Destroy(inputController);
                    };
                }
            }
        }
        public void getObjectsInBox(Vector3 start, Vector3 end){
            Debug.Log("dragselect ONMOUSEUP vectors:  " + start + " " + end);
            var bounds = util.DrawRectangle.GetViewportBounds(Camera.main,start,end);
            var fleets = util.DrawRectangle.getObjectsInBox<Fleet>(bounds);
            Debug.Log("fleets.length :" + fleets.Count);
            if (fleets.Count == 0 ){
                var planets = util.DrawRectangle.getObjectsInBox<Planet>(bounds);
                Debug.Log("planets.length:" + planets.Count);
            }else{
                inputController = fleets[0].getInputController(this.gameObject);
            }
        }
        
    }
}