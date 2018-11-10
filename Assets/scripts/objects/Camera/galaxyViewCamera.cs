using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galaxyViewCamera : MonoBehaviour
{
    public float flySpeed = 100.0f;
    public Vector2 flySpeedLimit = new Vector2(0, 10);
    public float rotationSpeed = 120.0f;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 mouseLookSensitivity = new Vector2(20, 20);
    public float zoomAmount = 2f;
    public Vector2 smoothing = new Vector2(2, 2);

    private Vector2 mouseDelta;
    private Vector2 mouseAbsolute;
    private Vector2 smoothMouse;

    void Update()
    {
        lookByRightClick();
        rotateByQE();
        zoomChangeByScroll();
        panByKeyBoard();
    }
    void panByKeyBoard()
    {
        if (Input.GetAxis("Vertical") != 0)
            transform.Translate(transform.forward * flySpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
        if (Input.GetAxis("Horizontal") != 0)
            transform.Translate(transform.right * flySpeed * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
    }
    void zoomChangeByScroll() {
        var input = Input.GetAxis("Mouse ScrollWheel");
        if (input > 0)
        {
            transform.Translate(transform.up * flySpeed * zoomAmount, Space.World);
        }
        else if (input < 0)
        {
            transform.Translate(transform.up * flySpeed * -1 * zoomAmount, Space.World);
        }
    }
    void rotateByQE()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    void lookByRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(mouseLookSensitivity.x * smoothing.x, mouseLookSensitivity.y * smoothing.y));
            smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1.0f / smoothing.x);
            smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1.0f / smoothing.y);
            mouseAbsolute = smoothMouse;

            transform.Rotate(-Vector3.right * mouseAbsolute.y * Time.deltaTime);
            transform.Rotate(Vector3.up * mouseAbsolute.x * Time.deltaTime);
        }
    }
}
