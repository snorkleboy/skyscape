using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBox : MonoBehaviour {

	public Vector3 clickDragStart;
	public Vector3 clickDragEnd;
	public bool _isDragging;
	public Color mainColor = new Color( 0.8f, 0.8f, 0.95f, 0.25f );
	public Color borderColor = new Color( 0.8f, 0.8f, 0.95f );
	public bool someSelected = false;
	public float thickness = 2;
	public System.Action onStartDrag;

	public System.Action<Vector3,Vector3> onMouseUp;

	public void Start(){
		_isDragging = false;
	}
	void Update()
    {
		if (_isDragging)
		{
			if (checkForDragEnd()){
				onDragEnd();
			}else{
				continueDragging();
			}
		}
		else		
		{
			if (checkForDragStart()){
				onDragStart();
			}
		}

    }
    void OnGUI()
    {
        if( _isDragging )
        {
			drawSelectionbox();
        }
    }
	private void drawSelectionbox(){
		var rect = util.Rectangle.GetScreenRect( clickDragStart, clickDragEnd );
		util.Rectangle.DrawScreenRect( rect,  mainColor);
		util.Rectangle.DrawScreenRectBorder( rect, thickness, borderColor);
	}

	bool checkForDragStart(){
		if (Input.GetMouseButtonDown(0)){
			return true;
		}
		return false;
	}
	bool checkForDragEnd(){
		if (Input.GetMouseButtonUp(0)){
			return true;
		}
		return false;
	}
	void continueDragging(){
		clickDragEnd = Input.mousePosition;
	}
	void onDragStart(){
		clickDragStart = Input.mousePosition;
		clickDragEnd = clickDragStart;
		if(onStartDrag != null){
			onStartDrag();		
		}
		_isDragging = true;
	}
	void onDragEnd(){
		clickDragEnd = Input.mousePosition;
		if (onMouseUp != null && !clickDragStart.Equals(clickDragEnd)){
			onMouseUp(clickDragStart,clickDragEnd);
		}

		_isDragging = false;
		clickDragEnd = Vector3.zero;
		clickDragStart = Vector3.zero;
	}

}
