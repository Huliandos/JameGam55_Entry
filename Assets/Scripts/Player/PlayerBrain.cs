using UnityEngine;

namespace Player{
    public abstract class PlayerBrain : MonoBehaviour
    {
        public static PlayerBrain Instance {get; private set;}

        [SerializeField] Limb[] _limbs;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                Debug.LogError($"[{GetType()}] Can't have multiple player instances");
                return;
            }

            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GrabableEnteredRange(Collider2D col)
        {
            Grabable grabable = col.GetComponent<Grabable>();

            if(grabable == null)
            {
                Debug.LogError($"[{GetType()}] Collision with {col} is missing grabable");
                return;
            }

            grabable.enabled = true;
        }
        
        public void GrabableLeftRange(Collider2D col)
        {
            Grabable grabable = col.GetComponent<Grabable>();

            if(grabable == null)
            {
                Debug.LogError($"[{GetType()}] Collision with {col} is missing grabable");
                return;
            }

            grabable.enabled = false;
        }

        public void AttachToGrabable(Grabable grabable)
        {
            Limb chosenLimb = _limbs[0];
            float chosenLimbAttachementDistance = chosenLimb.AttachementDistanceSq(grabable.transform.position);

            foreach(Limb limb in _limbs)
            {
                //Limb not attached means it's free to be used
                if (!limb.IsAttached)
                {
                    chosenLimb = limb;
                    break;
                }
                float curAttachementDistance = limb.AttachementDistanceSq(grabable.transform.position);
                //Choose furthest limb
                if(curAttachementDistance > chosenLimbAttachementDistance)
                {
                    chosenLimb = limb;
                    chosenLimbAttachementDistance = curAttachementDistance;
                }
            }

            chosenLimb.Detach();
            chosenLimb.Attach(grabable.transform);
        }

        public void DetachFromGrabable(Grabable grabable)
        {
            foreach(Limb limb in _limbs)
            {
                //Limb not attached means it's free to be used
                if (limb.AttachedTo == grabable.transform)
                {
                    limb.Detach();
                    break;
                }
            }
        }
    }
}