using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeAllUtil : MonoBehaviour {

    public GameObject target;

    public void SetLayerRecursively(Transform target)
    {
        Debug.Log("CHANGE LAYER");
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
