using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createFromClick : MonoBehaviour {

    public galaxyCreator galmaker;
    void OnMouseDown()
    {

        Debug.Log("click create");
        galmaker.create();
    }
}
