﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCreateMenu : MonoBehaviour {

    public GalaxyCreators.galaxyCreator galCreator;
    [ContextMenu("Create Galaxy")]
    public void makeGalaxy()
    {
        galCreator.create();
        SetLayerRecursively(galCreator.transform);
    }
    [ContextMenu("Destroy Galaxy")]
    public void destroy()
    {
        galCreator.destroy();
    }

    public void SetLayerRecursively(Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.gameObject.layer = 9;
        foreach (Transform child in target)
        {
            SetLayerRecursively(child);
        }
    }
}
