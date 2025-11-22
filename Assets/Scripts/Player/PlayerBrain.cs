using System;
using System.Collections;
using UnityEngine;

namespace Player{
    public class PlayerBrain : MonoBehaviour
    {
        public static PlayerBrain Instance {get; private set;}

        [SerializeField] Limb[] _limbs;

        [SerializeField] Collider2D _grabableRangeCollider;

        [SerializeField] float _stunDuration = 1;

        [SerializeField] FacialExpressionInfo[] _facialExpressions;

        [Header("Stunned fx")]
        [SerializeField] GameObject _stunFx;

        [SerializeField] AudioSource _audioSource;

        [SerializeField] AudioClip[] _stunnedSFX;

        Coroutine _stunnedCoroutine;

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

        public void GrabableEnteredRange(Collider2D col)
        {
            Holds.Grabable grabable = col.GetComponent<Holds.Grabable>();

            if(grabable == null)
            {
                Debug.LogError($"[{GetType()}] Collision with {col} is missing grabable");
                return;
            }

            grabable.enabled = true;
        }
        
        public void GrabableLeftRange(Collider2D col)
        {
            Holds.Grabable grabable = col.GetComponent<Holds.Grabable>();

            if(grabable == null)
            {
                Debug.LogError($"[{GetType()}] Collision with {col} is missing grabable");
                return;
            }

            grabable.enabled = false;
        }

        public void AttachToGrabable(Holds.Grabable grabable)
        {
            Limb chosenLimb = _limbs[0];
            float chosenLimbAttachementDistance = chosenLimb.AttachementDistanceSq(grabable.transform.position);

            foreach(Limb limb in _limbs)
            {
                //Limb not attached means it's free to be used
                if (limb.AttachedTo == null)
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

        public void DetachFromGrabable(Holds.Grabable grabable)
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

        [ContextMenu("Stun")]
        public void Stun()
        {
            _stunFx.SetActive(true);
            _audioSource.PlayOneShot(_stunnedSFX[UnityEngine.Random.Range(0, _stunnedSFX.Length)]);
            foreach(FacialExpressionInfo facialExpressionInfo in _facialExpressions)
            {
                facialExpressionInfo.ToggleObjectsToMyState(FacialExpressionInfo.State.STUNNED);
            }


            _grabableRangeCollider.enabled = false;

            if(_stunnedCoroutine != null)
            {
                StopCoroutine(_stunnedCoroutine);
            }
            _stunnedCoroutine = StartCoroutine(ReenableColliderCoroutine());

            DetachAllLimbs();
        }

        IEnumerator ReenableColliderCoroutine()
        {
            yield return new WaitForSeconds(_stunDuration);

            
            foreach(FacialExpressionInfo facialExpressionInfo in _facialExpressions)
            {
                facialExpressionInfo.ToggleObjectsToMyState(FacialExpressionInfo.State.NORMAL);
            }

            _grabableRangeCollider.enabled = true;
            _stunnedCoroutine = null;
        }

        void DetachAllLimbs()
        {
            foreach(Limb limb in _limbs)
                DetachLimb(limb);
        }

        void DetachLimb(Limb limb)
        {
            if(limb.AttachedTo == null){
                Debug.LogWarning($"[{GetType()}] Tried to detach limb that wasn't attached to anything");
                return;
            }
            DetachFromGrabable(limb.AttachedTo.GetComponent<Holds.Grabable>());
        }
    }

    [Serializable]
    struct FacialExpressionInfo
    {
        public enum State {NORMAL, STUNNED}

        [SerializeField] State _expressionState;

        [SerializeField] GameObject[] _expressionObjects;

        public void ToggleObjectsToMyState(State stateToCheckAgainst)
        {
            if(stateToCheckAgainst == _expressionState)
            {
                EnableObjects();
                return;
            }
            DisableObjects();
        }

        public void EnableObjects()
        {
            foreach(GameObject objectToEnable in _expressionObjects)
            {
                objectToEnable.SetActive(true);
            }
        }

        public void DisableObjects()
        {
            foreach(GameObject objectToEnable in _expressionObjects)
            {
                objectToEnable.SetActive(false);
            }
        }
    }
}
