using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
public class createFromClick : MonoBehaviour {

    public GalaxyCreators.galaxyCreator<ProtoStar> galmaker;
    void OnMouseDown()
    {

        galmaker.create();
    }
}
