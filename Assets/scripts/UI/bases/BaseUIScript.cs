using UnityEngine;
using Objects.Galaxy;
namespace UI
{
    public abstract class BaseUIScript<GalObj> : MonoBehaviour
    {		
        public int perFrameRate = 10;

		[SerializeField]protected GalObj _toDisplay;

		protected int lastUpdateId = -1;
        public virtual void set(GalObj obj){
			_toDisplay = obj;
			transform.gameObject.SetActive(true);
			render();
		}
		public virtual void unset(){
			transform.gameObject.SetActive(true);
		}
        
		protected virtual void LateUpdate() {
			if(Time.frameCount%perFrameRate == 0){
				refresh();
			}
		}
		protected abstract void refresh();
		abstract protected void render();
    }
}