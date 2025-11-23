using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Stopwatch : TimeTrackerComponent
{
    #region Public Properties
    
    public override TimeSpan Time => _time;
    public bool IsRunning => isRunning;
    public UnityEvent OnTimerBegin => onTimerBegin;

    #endregion
    
    #region Serialized Fields
    
    [Tooltip("Should the stopwatch begin counting on start?")]
    [FormerlySerializedAs("startOnAwake")] [SerializeField] private bool beginOnStart = true;
    [Tooltip("The initial time of the stopwatch.")]
    [SerializeField] private float initialTime = 0;
    [Tooltip("If true, the stopwatch will continue counting after being reset.")]
    [SerializeField] private bool stayActiveOnReset = true;
    [Tooltip("The event that is called when the timer begins.")]
    [SerializeField] private UnityEvent onTimerBegin;
    
    #endregion
    
    #region Private Fields
    
    private TimeSpan _time;
    private bool isRunning = false;

    #endregion

    #region Unity Events

    void Start()
    {
        if (beginOnStart)
        {
            Begin();
        }
    }

    void Update()
    {
        if (isRunning)
        {
            _time += TimeSpan.FromSeconds(UnityEngine.Time.deltaTime);
        }
    }

    #endregion
    
    #region Public Methods
    
    public override void Begin()
    {
        onTimerBegin?.Invoke();
        isRunning = true;
    }
    
    public override void Stop()
    {
        isRunning = false;
    }
    
    public override void Reset()
    {
        _time = TimeSpan.FromSeconds(initialTime);
        if (!stayActiveOnReset)
        {
            Stop();
        }
    }
    
    void OnGameReset()
    {
        Reset();
    }
    
    #endregion
}
