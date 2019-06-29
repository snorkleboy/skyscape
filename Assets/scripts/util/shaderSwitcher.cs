using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class shaderSwitcher : MonoBehaviour
{
    public Shader shaderToInject;
    public bool on = false;
    public MeshRenderer renderer;

    Shader temp;
    [ContextMenu("Toggle")]   
    public bool toggle(){
        if(on){
            turnOff();
        }else{
            turnOn();
        }
        return on;
    }
    private void turnOff(){
        renderer.material.shader = temp;
        temp = null;
        on = false;
    }
    private void turnOn(){
        temp = renderer.material.shader;
        renderer.material.shader = shaderToInject;
        on = true;
    }

}
