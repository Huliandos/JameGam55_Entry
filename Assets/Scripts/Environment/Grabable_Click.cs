using UnityEngine;
using UnityEngine.InputSystem;

namespace Holds{
    public class Grabable_Click : Grabable
    {
        bool _hasAttachedLimb; 

        protected override void KeyDown(InputAction.CallbackContext context)
        {
            if(!enabled)
                return;

            if (_hasAttachedLimb)
            {
                Player.PlayerBrain.Instance.DetachFromGrabable(this);
                _hasAttachedLimb = false;
                return;
            }
            Player.PlayerBrain.Instance.AttachToGrabable(this);
            _hasAttachedLimb = true;
        }

        protected override void KeyUp(InputAction.CallbackContext context)
        {
            //do nothing
        }

    }
}
