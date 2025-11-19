using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Grabable : MonoBehaviour
{
    public InputAction GrabableInputAction {get { return _grabableInputAction;}}

    bool _hasAttachedLimb; 

    [SerializeField] Image _instructionsImage;

    [SerializeField] bool _holdToAttach;

    [Header("Input generation")]
    [SerializeField] float _noDuplicateInputsRange = 10;
    InputAction _grabableInputAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, _noDuplicateInputsRange, 1 << gameObject.layer);

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

    private void UpdateSprite()
    {
        //_instructionsImage.text = _grabableInputAction.GetBindingDisplayString();
        _instructionsImage.sprite = InputManager.Instance.GetSpriteOfInputAction(_grabableInputAction);
    }

    void OnEnable()
    {
        _instructionsImage.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        _instructionsImage.gameObject.SetActive(false);
    }

    private void KeyDown(InputAction.CallbackContext context)
    {
        Debug.Log("Key down");
        if(!enabled)
            return;

        if (_holdToAttach)
        {
            Player.PlayerBrain.Instance.AttachToGrabable(this);
            return;
        }

        if (_hasAttachedLimb)
        {
            Player.PlayerBrain.Instance.DetachFromGrabable(this);
            _hasAttachedLimb = false;
            return;
        }
        Player.PlayerBrain.Instance.AttachToGrabable(this);
        _hasAttachedLimb = true;
    }

    private void KeyPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Key performed");
    }

    private void KeyUp(InputAction.CallbackContext context)
    {
        Debug.Log("Key up");
        if(!enabled)
            return;

        if (_holdToAttach)
        {
            Player.PlayerBrain.Instance.DetachFromGrabable(this);
            return;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Set the gizmo color
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _noDuplicateInputsRange);
    }
}
