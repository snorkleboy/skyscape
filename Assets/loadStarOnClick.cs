using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

public class loadStarOnClick : MonoBehaviour {

    public GameManager manager;
    public StarStub starHolder;
    void Awake()
    {
        manager = GetComponentInParent<GameManager>();
    }
    public void OnMouseDown()
    {
        Debug.Log("CLICK STAR");
        manager.loadStarSystem(starHolder);
    }
}
