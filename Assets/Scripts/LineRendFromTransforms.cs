using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendFromTransforms : MonoBehaviour
{
    [SerializeField] Transform[] _transforms;

    LineRenderer _lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.positionCount = _transforms.Length;

        for(int i=0; i<_transforms.Length; i++)
        {
            _lineRenderer.SetPosition(i, _transforms[i].position);
        }
    }
}
