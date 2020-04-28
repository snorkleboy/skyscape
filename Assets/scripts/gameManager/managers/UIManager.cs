
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Objects.Galaxy;

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

        public Fleet selectedFleet;
        public MonoBehaviour hoverObject;

        public DragBox dragSelect;

        public Texture2D attackCursor;
        public Texture2D ModCursor;
        public Texture2D moveCursor;

        public Texture2D currentCursorTexture;
        public Texture2D defaultCursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = new Vector2(20,7);
        public void Awake()
        {
            resetCursorTexture();
        }
        public void setCursorTexture(Texture2D tx)
        {
            currentCursorTexture = tx;
            Cursor.SetCursor(currentCursorTexture, hotSpot, cursorMode);
        }
        public void setCursorTexture(Texture2D tx, Vector2 hotSpot)
        {
            currentCursorTexture = tx;
            Cursor.SetCursor(currentCursorTexture, hotSpot, cursorMode);
        }
        public void resetCursorTexture()
        {
            currentCursorTexture = defaultCursorTexture;

            Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
        }

        private void Update() {
            objectinputController?.checkAction();
            cameraController?.checkAction();
        }
        public void setSelectedFleet(Fleet fleet)
        {
            selectedFleet = fleet;
            Debug.Log("set Selected Fleet " + fleet?.state.namedState.name ?? " null");
            if (fleet)
            {
                objectinputController = fleet.controller.startControl();
                var parent = mainUI.bottomBar;
                var ui = new GameObject("selectionThing");
                var text = ui.AddComponent<Text>();
                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                text.font = ArialFont;
                text.material = ArialFont.material;
                text.text = fleet.state.namedState.name;
                var pos = ui.GetComponent<RectTransform>();
                ui.SetParent(parent);
                pos.anchorMin = new Vector2(0, 0);
                pos.anchorMax = new Vector2(1, 1);
                pos.offsetMin = new Vector2(0, 0);
                pos.offsetMax = new Vector2(1, 1);

            }
            else
            {
                foreach (Transform child in mainUI.bottomBar.transform)
                {
                    Destroy(child.gameObject);
                }
                objectinputController?.endControl();
                objectinputController = null;
            }
        
        }
        public void setHoverObject(MonoBehaviour hoverObject)
        {
            this.hoverObject = hoverObject;
            objectinputController?.hoverResponse(hoverObject);
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
  //                  dragSelect.onStartDrag = ()=>{
  //                      objectinputController = null;
   //                 };
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
                setSelectedFleet(null);
                Debug.Log("planets.length:" + planets.Count);
            }else{
                setSelectedFleet(fleets[0]);
            }
        }
        
    }
}