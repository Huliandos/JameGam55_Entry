using System;
using UnityEngine;

namespace Player{
    public class Limb : MonoBehaviour
    {
        public Transform AttachedTo { get; private set; }

        [SerializeField] SpringJoint2D _attachementPoint;
        
        [SerializeField] LineRenderer _lineRenderer;

        public void Attach(Transform trans)
        {
            _attachementPoint.connectedBody = trans.GetComponent<Rigidbody2D>();
            _attachementPoint.enabled = true;

            AttachedTo = trans;
        }

        public void Detach()
        {
            _attachementPoint.connectedBody = null;
            _attachementPoint.enabled = false;

            AttachedTo = null;
        }

        public float AttachementDistanceSq(Vector3 pos)
        {
            if(AttachedTo == null)
                return Mathf.Infinity;

            return Vector3.SqrMagnitude(pos - AttachedTo.position);
        }
    }
}
