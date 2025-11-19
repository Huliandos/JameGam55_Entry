using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public enum DeviceType{ KEYBOARD, PLAYSTATION, XBOX, SWITCH, GENERIC_GAMEPAD}
    DeviceType _currentDeviceType;

    public Action OnDeviceChanged;

    [SerializeField] PlayerInput _playerInput;

    public InputAction[] GrabbingInputActions {get; private set;}

    [SerializeField] InputActionIconsMapping[] _inputActionIconsMappings;

    protected override void Awake()
    {
        base.Awake();

        List<InputAction> inputActions = new List<InputAction>();
        foreach(InputAction inputAction in _playerInput.actions)
        {
            if (inputAction.name.Contains("Button"))
            {
                inputActions.Add(inputAction);
                continue;
            }
        }
        GrabbingInputActions = inputActions.ToArray();

        ControlsChanged(_playerInput);

        _playerInput.onControlsChanged += ControlsChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _playerInput.onControlsChanged -= ControlsChanged;
    }

    public InputAction GetRandomInputAction()
    {
        return GrabbingInputActions[UnityEngine.Random.Range(0, GrabbingInputActions.Length)];
    }

    private void ControlsChanged(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            // Determine specific gamepad type
            Gamepad gamepad = Gamepad.current;
            
            if (gamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad ||
                gamepad is UnityEngine.InputSystem.DualShock.DualSenseGamepadHID ||
                gamepad is UnityEngine.InputSystem.DualShock.DualShock3GamepadHID ||
                gamepad is UnityEngine.InputSystem.DualShock.DualShock4GamepadHID)
            {
                Debug.Log($"[{GetType()}] Switchted to PlayStation Controller");

                _currentDeviceType = DeviceType.PLAYSTATION;
            }
            else if (gamepad is UnityEngine.InputSystem.XInput.XInputController ||
                gamepad is UnityEngine.InputSystem.XInput.IXboxOneRumble ||
                gamepad is UnityEngine.InputSystem.XInput.XInputControllerWindows)
            {
                Debug.Log($"[{GetType()}] Switchted to Xbox Controller");

                _currentDeviceType = DeviceType.XBOX;
            }
            else if (gamepad is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
            {
                Debug.Log($"[{GetType()}] Switchted to Nintendo Switch Controller");

                _currentDeviceType = DeviceType.SWITCH;
            }
            else
            {
                Debug.Log($"[{GetType()}] Switchted to Generic Gamepad");

                _currentDeviceType = DeviceType.GENERIC_GAMEPAD;
            }
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Debug.Log($"[{GetType()}] Switchted to Keyboard and Mouse");

            _currentDeviceType = DeviceType.KEYBOARD;
        }

        OnDeviceChanged?.Invoke();
    }

    public Sprite GetSpriteOfInputAction(InputAction inputAction)
    {
        foreach(InputActionIconsMapping mapping in _inputActionIconsMappings)
        {
            if(mapping.InputActionRef.action == inputAction)
            {
                return mapping.GetCurrentSprite(_currentDeviceType);
            }
        }

        Debug.LogError($"[{GetType()}] input action {inputAction} with undefined sprite");
        return null;
    }

    [Serializable]
    class InputActionIconsMapping
    {
        public InputActionReference InputActionRef { get {return _inputAction;} }
        [SerializeField] InputActionReference _inputAction;

        [SerializeField] Sprite _keyboardSprite, _playstationSprite, _xboxSprite, _switchSprite, _genericSprite;

        public Sprite GetCurrentSprite(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.KEYBOARD:
                    return _keyboardSprite;
                case DeviceType.PLAYSTATION:
                    return _playstationSprite;
                case DeviceType.XBOX:
                    return _xboxSprite;
                case DeviceType.SWITCH:
                    return _switchSprite;
                case DeviceType.GENERIC_GAMEPAD:
                    return _genericSprite;
            }
            Debug.LogError($"[{GetType()}] device type {deviceType} not defined.");
            return null;
        }
    }
}
