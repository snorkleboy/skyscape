using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCreateMenu : MonoBehaviour {

    public galaxyCreator galCreator;
    [ContextMenu("Create Galaxy")]
    public void makeGalaxy()
    {
        galCreator.create();
    }
    [ContextMenu("Destroy Galaxy")]
    public void destroy()
    {
        galCreator.destroy();
    }
}
