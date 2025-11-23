using TMPro;
using UnityEngine;

public class DisplayCounter : MonoBehaviour
{
    [Tooltip("The text component to display the time.")]
    [SerializeField] private TMP_Text text;
    
    int _counter;

    public void UpdateCounter(int newCount)
    {
        _counter = newCount;
        text.text = _counter.ToString();
    }
}
