using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is used to display the time of a TimeTrackerComponent in a text component.
/// Modified to display only minutes and seconds.
/// </summary>
public class TimeDisplayTextAdjustable : MonoBehaviour
{
    [Tooltip("The format of the time display (TimeSpan format string).")]
    [SerializeField] private string format = "mm\\:ss";
    [Tooltip("The text component to display the time.")]
    [SerializeField] private TMP_Text text;
    [Tooltip("The TimeTrackerComponent to get the time from.")]
    [SerializeField] private TimeTrackerComponent timeTracker;
    [Tooltip("Spacing value around the ':' symbol.")]
    [SerializeField] private float colonSpacing = 0.25f;  // Adjust this value as needed.
    [Tooltip("Spacing value around the '.' symbol (unused, kept for compatibility).")]
    [SerializeField] private float periodSpacing = 0.25f;  // Kept for compatibility.

    // Cached strings to avoid repeated allocations
    private string _cachedColonReplacement;
    private string _cachedPeriodReplacement; // Kept but unused
    
    // StringBuilder for efficient string building
    private StringBuilder _stringBuilder = new StringBuilder(32);

    // Update frequency control
    [Tooltip("How many times per second to update the display. <= 0 means every frame")]
    [SerializeField] private float updateFrequency = 10f;
    private float _nextUpdateTime;

    void Start()
    {
        // Pre-build the cached replacement strings
        UpdateCachedReplacements();
        _nextUpdateTime = Time.time;
    }

    void Update()
    {
        // Limit update frequency
        if (updateFrequency > 0 && Time.time < _nextUpdateTime)
            return;

        if (updateFrequency > 0)
            _nextUpdateTime = Time.time + (1f / updateFrequency);

        TimeSpan currentTime = timeTracker.Time;

        // Extract time components manually
        int minutes = currentTime.Minutes;
        int seconds = currentTime.Seconds;

        BuildTimeStringManually(minutes, seconds);
    }

    private void BuildTimeStringManually(int minutes, int seconds)
    {
        _stringBuilder.Clear();

        // Minutes (always 2 digits)
        char minutesTens = (char)('0' + (minutes / 10));
        char minutesOnes = (char)('0' + (minutes % 10));
        _stringBuilder.Append(minutesTens);
        _stringBuilder.Append(minutesOnes);

        // Colon with spacing
        _stringBuilder.Append(_cachedColonReplacement);

        // Seconds (always 2 digits)
        char secondsTens = (char)('0' + (seconds / 10));
        char secondsOnes = (char)('0' + (seconds % 10));
        _stringBuilder.Append(secondsTens);
        _stringBuilder.Append(secondsOnes);

        // No period or hundredths displayed anymore

        // Update the text
        text.text = _stringBuilder.ToString();
    }

    private void UpdateCachedReplacements()
    {
        _cachedColonReplacement = $"<cspace={colonSpacing.ToString()}> : </cspace>";
        _cachedPeriodReplacement = $"<cspace={periodSpacing.ToString()}> . </cspace>"; // Unused, kept for compatibility
    }

    // Call this if you change spacing values at runtime
    public void RefreshSpacingCache()
    {
        UpdateCachedReplacements();
        _nextUpdateTime = Time.time;
    }

    void OnValidate()
    {
        // Update cached strings when values change in inspector
        if (Application.isPlaying)
        {
            UpdateCachedReplacements();
        }
    }
}
