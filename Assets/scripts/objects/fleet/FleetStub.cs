using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Objects;
using UI;
public class FleetStub : MonoBehaviour {
    private GameObject floatingIcon;
    private GameObject canvas;
    public Fleet fleet;
    private Renderer m_Renderer;

    public void Start(){
        m_Renderer = transform.gameObject.GetComponentInChildren<MeshRenderer>();
    }
    public void set(Fleet fleet){
        this.fleet = fleet;

    }
    public void Update(){

        if (canvas == null){
            canvas = GameManager.instance.sceneCanvas.gameObject;
        }

        if (floatingIcon == null){
            floatingIcon = UIComponents.renderIconLabel(fleet.getIconableInfo());
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
