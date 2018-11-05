using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UI;
public class StarStub : Stub {
    public void set(StarNode starnode){
        this.starnode = starnode;
        var icon = GetComponentInChildren<SpaceableIcon>();
        if (icon != null){
            Debug.Log("HERE"+icon);
            icon.set(starnode);
        }
    }
    public StarNode starnode;

}
