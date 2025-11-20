using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DestroyObjectInSeconds(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    public void DestroyComponent(Component component)
    {
        Destroy(component);
    }

    public void DestroyComponentInSeconds(Component component, float seconds)
    {
        Destroy(component, seconds);
    }
}
