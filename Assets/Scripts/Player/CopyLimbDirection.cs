using UnityEngine;

public class CopyLimbDirection : MonoBehaviour
{
    [SerializeField] Transform LimbEnd;
    [SerializeField] Transform LimbMiddle;

    void Update()
    {
        Vector2 dir = LimbEnd.position - LimbMiddle.position;

        if (dir.sqrMagnitude < 0.001f)
            return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (transform.parent != null)
        {
            transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}