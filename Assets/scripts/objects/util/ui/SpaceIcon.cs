using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Objects;
using UI;
public class SpaceIcon : MonoBehaviour {

    private GameObject floatingIcon;
    private GameObject canvas;
    public IUIable uiable;
    private Renderer m_Renderer;
	public int renderDistance =  700;
	public System.Func<iconInfo,GameObject> iconCallBack =  UIComponents.renderIconLabel;
	public Vector3 offset = new Vector3(0,12,0);
    public virtual void Start(){
		m_Renderer = gameObject.GetComponent<MeshRenderer>();
		if (!m_Renderer){
			m_Renderer = gameObject.GetComponentInChildren<MeshRenderer>();
		}
		if (!m_Renderer){
			m_Renderer = gameObject.GetComponentInParent<MeshRenderer>();
		}
		if (!m_Renderer){
			Debug.LogWarning("spaceIcon couldnt get m_Renderer  " + m_Renderer);
		}
        uiable = gameObject.GetComponentInParent<IUIable>();
		if (uiable == null){
			Debug.LogWarning("spaceIcon couldnt get uiable  " + uiable);
		}
    }
    public void LateUpdate(){
		if (GameManager.instance.UIManager.sceneCanvas && (canvas = GameManager.instance.UIManager.sceneCanvas.gameObject)){
			if (floatingIcon == null){
				floatingIcon = iconCallBack(uiable.getIconableInfo());
				floatingIcon.transform.SetParent(canvas.transform);
				setPosition();
			}
			if(shouldRender()){
				setPosition();
				floatingIcon.SetActive(true);
			}else{
				floatingIcon.SetActive(false);
			}
		}

    }
	private void setPosition(){
		var pos = Camera.main.WorldToScreenPoint(getTargetPosition() + offset);
		floatingIcon.transform.position = (pos);
	}
	protected virtual bool shouldRender(){
		return m_Renderer.isVisible && Vector3.Distance(Camera.main.transform.position, transform.position)< renderDistance;
	}
	protected virtual Vector3 getTargetPosition(){
		return transform.position;
	}
}
