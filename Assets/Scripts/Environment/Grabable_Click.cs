using UnityEngine;
using UnityEngine.InputSystem;

namespace Holds{
    public class Grabable_Click : Grabable
    {
        protected override void KeyDown(InputAction.CallbackContext context)
        {
            //can let go if not enabled
            if(!enabled && !_hasAttachedLimb)
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
