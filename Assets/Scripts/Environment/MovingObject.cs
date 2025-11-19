using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] float _timeToTravelBetweenPositions = 1;

    [Tooltip("Relative to spawn pos, only relevant until initialized")]
    [SerializeField] Vector3 _relativeStartingPos, _relativeEndPos;

    Vector3 _worldStartingPos, _worldEndPos;

    bool _reversePath;

    float _pathProgress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _worldStartingPos = transform.position + _relativeStartingPos;
        _worldEndPos = transform.position + _relativeEndPos;
        
        transform.position = _worldStartingPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _pathProgress += Time.fixedDeltaTime / _timeToTravelBetweenPositions;
        if(_pathProgress >= 1)
        {
            _pathProgress -=1;
            _reversePath = !_reversePath;
        }

        if (_reversePath)
        {
            transform.position = Vector3.Lerp(_worldEndPos, _worldStartingPos, _pathProgress);
            return;
        }

        transform.position = Vector3.Lerp(_worldStartingPos, _worldEndPos, _pathProgress);
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + _relativeStartingPos, transform.position + _relativeEndPos);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + _relativeStartingPos, .25f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _relativeEndPos, .25f);
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_worldStartingPos, _worldEndPos);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_worldStartingPos, .25f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_worldEndPos, .25f);
    }
}
