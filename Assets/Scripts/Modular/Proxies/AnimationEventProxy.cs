using UnityEngine;
using UnityEngine.Events;

public class AnimationEventProxy : MonoBehaviour
{
    public UnityEvent OnAnimationEvent;

    public void AnimationEvent()
    {
        OnAnimationEvent?.Invoke();
    }
}
