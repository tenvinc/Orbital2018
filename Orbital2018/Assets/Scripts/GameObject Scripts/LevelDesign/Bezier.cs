using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour {

    public LineRenderer lineRenderer;
    public Transform point0, point1;

    private int numPoints = 50;
    private int numCapVertices = 7;
    private int numCornerVertices = 1;
    private Vector3[] positions = new Vector3[50];

    private Vector3 CalculateLinearBezierPoint (float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private void DrawLinearCurve ()
    {
        float t;
        for (int i = 1; i <= numPoints; i++)
        { 
            t = i / (float)numPoints;
            positions[i-1] = CalculateLinearBezierPoint(t, point0.position, point1.position); 
        }
        lineRenderer.SetPositions(positions);
    }

	// Use this for initialization
	void Start () {
        lineRenderer.numCapVertices = numCapVertices;
        lineRenderer.numCornerVertices = numCornerVertices;
        lineRenderer.positionCount = numPoints;
        DrawLinearCurve();
	}
	
	// Update is called once per frame
	void Update () {
    }
}
