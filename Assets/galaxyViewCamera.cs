using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galaxyViewCamera : MonoBehaviour {
    [Header("Movement Speeds")]
    [Space]
    public float minPanSpeed;
    public float maxPanSpeed;
    public float secToMaxSpeed; //seconds taken to reach max speed;
    public float zoomSpeed;

    [Header("Movement Limits")]
    [Space]
    public bool enableMovementLimits;
    public Vector2 heightLimit;
    public Vector2 lenghtLimit;
    public Vector2 widthLimit;
    private Vector2 zoomLimit;

    private float panSpeed;
    private Vector3 initialPos;
    private Vector3 panMovement;
    private Vector3 pos;
    private Quaternion rot;
    private bool rotationActive = false;
    private Vector3 lastMousePosition;
    private Quaternion initialRot;
    private float panIncrease = 0.0f;

    [Header("Rotation")]
    [Space]
    public bool rotationEnabled;
    public float rotateSpeed;

    // Use this for initialization
    void Start () {
        initialPos = transform.position;
        initialRot = transform.rotation;
        zoomLimit.x = 15;
        zoomLimit.y = 65;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
