using UnityEngine;

namespace Player{
    public class Limb : MonoBehaviour
    {
        public bool IsAttached { get; private set;}

        public Transform AttachedTo { get; private set; }

        [SerializeField] SpringJoint2D _attachementPoint;
        
        [SerializeField] LineRenderer _lineRenderer;

        [SerializeField] Transform _lineAttachementRef;

        public void Attach(Transform trans)
        {
            _attachementPoint.connectedBody = trans.GetComponent<Rigidbody2D>();
            _attachementPoint.enabled = true;

            _lineAttachementRef.transform.parent = trans;
            _lineAttachementRef.transform.localPosition = Vector3.zero;
            _lineAttachementRef.transform.localRotation = Quaternion.identity;

            IsAttached = true;
            AttachedTo = trans;
        }

        public void Detach()
        {
            _attachementPoint.connectedBody = null;
            _attachementPoint.enabled = false;

            _lineAttachementRef.transform.parent = transform;
            _lineAttachementRef.transform.localPosition = Vector3.zero;
            _lineAttachementRef.transform.localRotation = Quaternion.identity;

            IsAttached = false;
            AttachedTo = null;
        }

        

        public float AttachementDistanceSq(Vector3 distanceTo)
        {
            return Vector3.SqrMagnitude(distanceTo - _lineAttachementRef.position);
        }
    }
}
