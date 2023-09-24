using UnityEditor;
using UnityEngine;

public class LineErasable : Erasable
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Erase(Collider other)
    {
        if (IsCloseToLine(other.transform.position))
            base.Erase(other);
    }

    private bool IsCloseToLine(Vector2 eraserPosition)
    {
        Vector3[] nodes = new Vector3[2];
        _lineRenderer.GetPositions(nodes);
        if (Vector2.Distance(nodes[0], nodes[1]) < 0.5F)
            return true;
        float distance = HandleUtility.DistancePointToLine(eraserPosition, nodes[0], nodes[1]);
        return distance < 0.5F;
    }
}