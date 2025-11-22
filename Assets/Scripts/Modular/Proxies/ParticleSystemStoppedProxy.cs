using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemStoppedProxy : MonoBehaviour
{
    public UnityEvent OnParticleSystemStoppedEvent;

    public void OnParticleSystemStopped()
    {
        OnParticleSystemStoppedEvent?.Invoke();
    }
}
