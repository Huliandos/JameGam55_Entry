using UnityEngine;
using UnityEngine.Events;

public class CollisionProxy : MonoBehaviour
{
    [SerializeField] LayerMask _collisionLayerMask;

    public UnityEvent<Collider2D> OnTriggerEnter, OnTriggerStay, OnTriggerExit;
    public UnityEvent<Collision2D> OnCollisionEnter, OnCollisionStay, OnCollisionExit;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _collisionLayerMask) == 0)
            return;

        OnCollisionEnter?.Invoke(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _collisionLayerMask) == 0)
            return;
            
        OnCollisionStay?.Invoke(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _collisionLayerMask) == 0)
            return;

        OnCollisionExit?.Invoke(collision);
    }
}
