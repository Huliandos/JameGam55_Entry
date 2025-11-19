using UnityEngine;
using UnityEngine.InputSystem;

namespace Holds{
    public class Grabable_Hold : Grabable
    {
        protected override void KeyDown(InputAction.CallbackContext context)
        {
            if(!enabled)
                return;

            Player.PlayerBrain.Instance.AttachToGrabable(this);
        }

        protected override void KeyUp(InputAction.CallbackContext context)
        {
            if(!enabled)
                return;

            Player.PlayerBrain.Instance.DetachFromGrabable(this);
        }

    }
}
