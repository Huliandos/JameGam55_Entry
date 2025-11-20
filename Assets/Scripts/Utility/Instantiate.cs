using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    [SerializeField] Vector3 _posOffset, _rotOffset;

    public void InstantiatePrefab()
    {
        if(_prefab == null)
        {
            Debug.LogWarning($"[{GetType()}] Trying to instantiate without prefab setup");
            return;
        }
        Instantiate(_prefab, _posOffset + transform.position, transform.rotation * Quaternion.Euler(_rotOffset));
    }
}
