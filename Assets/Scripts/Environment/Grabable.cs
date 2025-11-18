using TMPro;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    bool _hasAttachedLimb; 

    [SerializeField] KeyCode _inputKey;

    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] bool _holdToAttach;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text.text = _inputKey.ToString();
    }

    void OnEnable()
    {
        _text.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        _text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_holdToAttach)
        {
            if (Input.GetKeyDown(_inputKey))
            {
                Player.PlayerBrain.Instance.AttachToGrabable(this);
            }
            if (Input.GetKeyUp(_inputKey))
            {
                Player.PlayerBrain.Instance.DetachFromGrabable(this);
            }
            return;
        }

        if (Input.GetKeyDown(_inputKey))
        {
            if (_hasAttachedLimb)
            {
                Player.PlayerBrain.Instance.DetachFromGrabable(this);
                _hasAttachedLimb = false;
                return;
            }
            Player.PlayerBrain.Instance.AttachToGrabable(this);
            _hasAttachedLimb = true;
        }
    }
}
