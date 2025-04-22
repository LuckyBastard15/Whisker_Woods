using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public RectTransform canvasTransform;
    public LineRenderer lineRenderer;

    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        lineRenderer.positionCount = 0;
    }

    public void AddPoint(RectTransform rectTransform)
    {
        Vector3 worldPos = rectTransform.position;
        points.Add(worldPos);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void ResetLine()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    }
}
