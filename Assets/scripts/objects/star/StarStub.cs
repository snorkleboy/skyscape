﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Objects;
using UI;
public class StarStub : MonoBehaviour {
    private GameObject floatingIcon;
    private GameObject canvas;
    public StarNode starnode;
    private Renderer m_Renderer;

    public void Start(){
        m_Renderer = transform.gameObject.GetComponentInChildren<MeshRenderer>();
        this.starnode = gameObject.GetComponentInParent<StarNode>();
    }
    public void Update(){

        if (canvas == null){
            canvas = GameManager.instance.sceneCanvas.gameObject;
        }

        if (floatingIcon == null){
            floatingIcon = UIComponents.renderIconLabel(starnode.getIconableInfo());
            floatingIcon.transform.SetParent(canvas.transform);
        }

        if(m_Renderer.isVisible && Vector3.Distance(Camera.main.transform.position, transform.position)< 700){
            floatingIcon.SetActive(true);
            var pos = Camera.main.WorldToScreenPoint(transform.position+ new Vector3(0,12,0));
            floatingIcon.transform.position = (pos);
        }else{
            floatingIcon.SetActive(false);
        }

    }

}
