using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum dragSelectState
{
	notDragging,
	dragging
}
public class DragSelect : MonoBehaviour {

	public Vector3 clickDragStart;
	public Vector3 clickDragEnd;
	public dragSelectState state;

	public bool someSelected = false;

	public void Start(){
		state = dragSelectState.notDragging;
	}
	public bool active = true;
	void Update()
    {
		if(state == dragSelectState.notDragging){
			if (checkForDragStart()){
				onDragStart();
			}
		}else if (state == dragSelectState.dragging){
			if (checkForDragEnd()){
				onDragEnd();
			}else{
				processDragging();
			}
		}
    }
	public bool checkForDragStart(){
		if (Input.GetMouseButtonDown(0)){
			return true;
		}
		return false;
	}
	public bool checkForDragEnd(){
		if (Input.GetMouseButtonUp(0)){
			return true;
		}
		return false;
	}
	public void processDragging(){
		Debug.Log("dragging");
		clickDragEnd = Input.mousePosition;
	}
	public void onDragStart(){
		Debug.Log("Pressed primary button.");
		clickDragStart = Input.mousePosition;
		state = dragSelectState.dragging;
	}
	public void onDragEnd(){
		Debug.Log("released");
		clickDragEnd = Input.mousePosition;
		state = dragSelectState.notDragging;
	}

}
