using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Objects.Galaxy;
using GalaxyCreators;
using System.IO;
using UI;
namespace UI
{
    public class InputReceiver : MonoBehaviour
    {
        public System.Action OnMouseEnterCB;
        public System.Action OnMouseExitCB;
        public System.Action OnMouseDownCB;
        public Objects.IInputReceiverTarget target;
        public void Awake()
        {
            target = GetComponentInParent<Objects.IInputReceiverTarget>();
            target.configureInputReciever(this);
        }
        protected void OnMouseEnter()
        {
            OnMouseEnterCB?.Invoke();
        }
        protected void OnMouseExit()
        {
            OnMouseExitCB?.Invoke();
        }
        protected void OnMouseDown()
        {
            OnMouseDownCB?.Invoke();
        }

    }
}
