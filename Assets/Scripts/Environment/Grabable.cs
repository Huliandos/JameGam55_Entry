using UnityEngine;

public class Grabable : MonoBehaviour
{
    [SerializeField] KeyCode _inputKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_inputKey))
        {
            
        }
    }
}
