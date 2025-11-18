using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] LayerMask _collisionLayerMask;

    public UnityEvent<Collider2D> OnTriggerEnter, OnTriggerStay, OnTriggerExit;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & _collisionLayerMask) == 0)
            return;

        OnTriggerEnter?.Invoke(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & _collisionLayerMask) == 0)
            return;
            
        OnTriggerStay?.Invoke(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & _collisionLayerMask) == 0)
            return;

        OnTriggerExit?.Invoke(collider);
    }
}
