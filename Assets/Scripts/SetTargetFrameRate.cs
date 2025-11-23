using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    [SerializeField] int _targetFrameRate = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
