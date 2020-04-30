using System;
using System.Collections.Generic;
using System.Text;
using Game.Core.State;
namespace Game.Core.Entities
{

    [System.Serializable]
    public partial class Fleet : BaseGameObject<FleetState>
    {

        public void init(FleetState state)
        {
            this.state = state;
        }


        //public void OnMouseEnterShip(Ship ship)
        //{
        //    Debug.Log("FLEET MOUSE ENTER" + ship.state.namedState.name);
        //    GameManager.instance.UIManager.setHoverObject(this);
        //    if (this.isUsers())
        //    {
        //        var switchers = GetComponentsInChildren<shaderSwitcher>();
        //        foreach (var switcher in switchers)
        //        {
        //            switcher.toggle();
        //        }
        //    }

        //}
        //public void OnMouseExitShip(Ship ship)
        //{
        //    Debug.Log("FLEET MOUSE Leavee" + ship.state.namedState.name);
        //    GameManager.instance.UIManager.setHoverObject(null);
        //    if (this.isUsers())
        //    {
        //        var switchers = GetComponentsInChildren<shaderSwitcher>();
        //        foreach (var switcher in switchers)
        //        {
        //            switcher.toggle();
        //        }
        //    }
        //}
        //public void OnMouseDownShip(Ship ship)
        //{
        //    Debug.Log("FLEET MOUSE Click" + ship.state.namedState.name + " " + this.state.namedState.name);
        //    if (this.isUsers())
        //    {
        //        GameManager.instance.UIManager.setSelectedFleet(this);
        //    }
        //}
    }

}
