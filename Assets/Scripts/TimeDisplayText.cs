using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is used to display the time of a TimeTrackerComponent in a text component.
/// </summary>
public class TimeDisplayText : MonoBehaviour
{
    [Tooltip("The format of the time display (TimeSpan format string).")]
    [SerializeField] private string format = "mm\\:ss\\.ff";
    [Tooltip("The text component to display the time.")]
    [SerializeField] private TMP_Text text;
    [Tooltip("The TimeTrackerComponent to get the time from.")]
    [SerializeField] private TimeTrackerComponent timeTracker;
    
    void Update()
    {
        text.text = timeTracker.Time.ToString(format);
    }
}
