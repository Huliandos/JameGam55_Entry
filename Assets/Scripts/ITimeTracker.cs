using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a base class for components that track time.
/// </summary>
[Serializable]
public abstract class TimeTrackerComponent : MonoBehaviour
{
    public abstract TimeSpan Time { get; }

    public abstract void Begin();

    public abstract void Stop();

    public abstract void Reset();
}
