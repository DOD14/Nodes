using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    //public Vector3 startPos;
    //public Vector3 endPos;

    [HideInInspector]
    public LineRenderer lineRenderer;

    // Use this for initialization
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
    /*
	public void SetPositions()
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
    */

