using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Holds{
    public abstract class Grabable : MonoBehaviour
    {
        public InputAction GrabableInputAction {get { return _grabableInputAction;}}

        [SerializeField] Image _instructionsImage;

        [Header("Input generation")]
        [SerializeField] float _noDuplicateInputsRange = 10;
        InputAction _grabableInputAction;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, _noDuplicateInputsRange, 1 << gameObject.layer);

            Debug.Log($"{collidersInRange == null} {InputManager.Instance.GrabbingInputActions == null}");

            if(collidersInRange.Length >= InputManager.Instance.GrabbingInputActions.Length)
            {
                Debug.LogError($"[{GetType()}] {gameObject} has more neighbors than legal grabbing input actions are allowed. Disabling");
                gameObject.SetActive(false);
                return;
            }

            bool inputAlreadyAssigned = true;
            InputAction randomInputAction = null;
            while (inputAlreadyAssigned){
                randomInputAction = InputManager.Instance.GetRandomInputAction();
                inputAlreadyAssigned = false;
                foreach(Collider2D collider in collidersInRange)
                {
                    Grabable grabable = collider.GetComponent<Grabable>();
                    if(grabable.GrabableInputAction == randomInputAction)
                    {
                        inputAlreadyAssigned = true;
                        break;
                    }
                }
            }

            _grabableInputAction = randomInputAction;

            _grabableInputAction.performed += KeyDown;
            _grabableInputAction.canceled += KeyUp;

            InputManager.Instance.OnDeviceChanged += UpdateSprite;
            UpdateSprite();
        }

        protected virtual void UpdateSprite()
        {
            //_instructionsImage.text = _grabableInputAction.GetBindingDisplayString();
            _instructionsImage.sprite = InputManager.Instance.GetSpriteOfInputAction(_grabableInputAction);
        }

        protected virtual void OnEnable()
        {
            _instructionsImage.gameObject.SetActive(true);
        }

        protected virtual void OnDisable()
        {
            _instructionsImage.gameObject.SetActive(false);
        }

        protected abstract void KeyDown(InputAction.CallbackContext context);

        protected abstract void KeyUp(InputAction.CallbackContext context);

        void OnDrawGizmosSelected()
        {
            // Set the gizmo color
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, _noDuplicateInputsRange);
        }
    }
}