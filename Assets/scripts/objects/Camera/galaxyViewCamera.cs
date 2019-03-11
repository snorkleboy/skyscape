using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
public class galaxyViewCamera : InputController
{
    public float flySpeed = 100.0f;
    public Vector2 flySpeedLimit = new Vector2(0, 10);
    public float rotationSpeed = 120.0f;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 mouseLookSensitivity = new Vector2(20, 20);
    public float zoomAmount = 2f;
    public Vector2 smoothing = new Vector2(2, 2);

    public Vector2 verticalRotateLimits = new Vector2(-10,70);
    public float squareSpaceSize = 9999;
    public Vector2 heightLimits = new Vector2(5,150);

    public void Awake(){
        controls = new List<inputAction>(){
            new inputAction(panHorizontalcheck,panHorizontal),
            new inputAction(panVerticalCheck,panVertical),
            new inputAction(zoomPositiveCheck,zoomPositive),
            new inputAction(zoomNegativeCheck,zoomNegative),
            new inputAction(rotateHorizontalPostiveCheck,rotateHorizontalPostive),
            new inputAction(rotateHorizontalNegativeCheck,rotateHorizontalNegative),
            new inputAction(rotateVerticalPostiveCheck,rotateVerticalPostive),
            new inputAction(rotateVerticalNegativeCheck,rotateVerticalNegative),
            new inputAction(freeLookCheck,freeLook),
        };
    }
    private void heightClamp(){
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,heightLimits.x,heightLimits.y),transform.position.z);
    }
    private void verticalRotateClamp(){
        transform.localEulerAngles = new Vector3(
            clampAngle(transform.localEulerAngles.x,verticalRotateLimits.x,verticalRotateLimits.y),
            transform.localEulerAngles.y,
            0
        );
    }
    public float clampAngle(float angle, float min, float max){
        if (angle<90 || angle>270){ 
            if (angle>180) angle -= 360;   
        }    
        angle = Mathf.Clamp(angle, min,max);
        if (angle<0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }
    private void tiltClamp(){
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,0);
    }

    bool panHorizontalcheck(){
        return Input.GetAxis("Horizontal") != 0;
    }
    void panHorizontal(){
        transform.Translate(transform.right * flySpeed * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
    }
    bool panVerticalCheck(){
        return Input.GetAxis("Vertical") != 0;
    }
    void panVertical(){
        var forward = transform.forward;
        forward.y = 0;
        transform.Translate(forward * flySpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
    }



    bool zoomPositiveCheck(){
        return 
            Input.GetAxis("Mouse ScrollWheel") > 0
            ||
            Input.GetKey(KeyCode.Plus)
        ;
    }
    void zoomPositive(){
        transform.Translate(Vector3.up  * zoomAmount, Space.World);
        heightClamp();
    }
    bool zoomNegativeCheck(){
        return 
            Input.GetAxis("Mouse ScrollWheel") < 0
            ||
            Input.GetKey(KeyCode.Minus)
        ;
    }
    void zoomNegative(){
        transform.Translate(Vector3.up  * -1 * zoomAmount, Space.World);
        heightClamp();
    }


    bool rotateVerticalNegativeCheck(){
        return Input.GetKey(KeyCode.Z);
    }
    void rotateVerticalNegative()
    {
        transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime,Space.Self);
        verticalRotateClamp();
    }
    bool rotateVerticalPostiveCheck(){
        return Input.GetKey(KeyCode.C);
    }
    void rotateVerticalPostive()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime,Space.Self);
        verticalRotateClamp();
    }


    bool rotateHorizontalNegativeCheck(){
        return Input.GetKey(KeyCode.Q);
    }
    void rotateHorizontalNegative()
    {
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime,Space.World);
        tiltClamp();
    }

    bool rotateHorizontalPostiveCheck(){
        return Input.GetKey(KeyCode.E);
    }
    void rotateHorizontalPostive()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime,Space.World);
        tiltClamp();
    }
    bool freeLookCheck(){
        return Input.GetMouseButton(1);
    }
    void freeLook()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(mouseLookSensitivity.x * smoothing.x, mouseLookSensitivity.y * smoothing.y));
        transform.Rotate(Vector3.up * mouseDelta.x * Time.deltaTime,Space.World);
        transform.Rotate(-Vector3.right * mouseDelta.y * Time.deltaTime, Space.Self);
        verticalRotateClamp();
    }
}
