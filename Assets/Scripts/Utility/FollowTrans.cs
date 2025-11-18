using UnityEngine;

public class FollowTrans : MonoBehaviour
{
    [SerializeField] Transform _trans;

    [SerializeField] Vector3 _offset;

    [SerializeField] float smoothTime = 0.3F;

    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, _trans.position + _offset, ref velocity, smoothTime);
    }
}
