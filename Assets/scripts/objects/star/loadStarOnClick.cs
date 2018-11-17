using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using Objects.Galaxy;

public class loadStarOnClick : MonoBehaviour {

    public GameManager manager;
    public StarNode star;
    void Awake()
    {
        manager = GetComponentInParent<GameManager>();
        star = GetComponentInParent<StarNode>();

    }
    public void OnMouseDown()
    {
        Debug.Log("CLICK STAR");
        manager.loadStarSystem(star);
    }
}
