using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    [HideInInspector]
    public LineRenderer lineRenderer;

    private Node startNode;
    private Node endNode;

    // Use this for initialization
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStartPos(Node node)
    {
        startNode = node;
    }

    public void SetEndPos(Node node)
    {
        endNode = node;
    }

    public void UpdatePositions()
    {
        lineRenderer.SetPosition(0, startNode.transform.position);
        lineRenderer.SetPosition(1, endNode.transform.position);
    }


}
  
	

