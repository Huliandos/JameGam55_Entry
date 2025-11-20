using UnityEngine;
using UnityEngine.Events;

public class EventPeriodically : MonoBehaviour
{
    public UnityEvent _periodicEvent;

    [SerializeField] float _initialWait, _eventPeriod;

    float _time;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if(_time < _initialWait)
            return;
        
        if(_time >= _initialWait + _eventPeriod)
        {
            _time -= _eventPeriod;
            _periodicEvent?.Invoke();
        }
    }
}
