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
        float distance = GetDistanceFromPointToLine(eraserPosition, nodes[0], nodes[1]);
        return distance < 0.5F;
    }

    private float GetDistanceFromPointToLine(Vector2 eraserPosition, Vector3 start, Vector3 end)
    {
        float a = Vector2.Distance(start, end);
        float b = Vector2.Distance(start, eraserPosition);
        float c = Vector2.Distance(end, eraserPosition);
        float s = (a + b + c) / 2F;
        return 2 * Mathf.Sqrt(s * (s - a) * (s - b) * (s - c)) / a;

        //a = dist(startp, endp)
        //b = dist(startp, p)
        //c = dist(endp, p)
        //s = (a + b + c) / 2
        //distance = 2 * sqrt(s(s - a)(s - b)(s - c)) / a
    }
}