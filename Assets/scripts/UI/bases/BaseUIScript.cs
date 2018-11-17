using UnityEngine;
using Objects.Galaxy;
namespace UI
{
    public abstract class BaseUIScript : MonoBehaviour
    {		
        public int perFrameRate = 10;

		[SerializeField]protected IViewable _toDisplay;

		protected int lastUpdateId = -1;
        public virtual void set(IViewable obj){
			_toDisplay = obj;
			transform.gameObject.SetActive(true);
			Debug.Log("PLANET VIEW SET " + obj);
			render();
		}
		public virtual void unset(){
			transform.gameObject.SetActive(true);
		}
        

		protected abstract void refresh();
		abstract protected void render();
    }
}