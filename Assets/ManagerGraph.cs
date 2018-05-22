using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGraph : MonoBehaviour
{
    public SimpleObjectPool nodePool;
    public SimpleObjectPool edgePool;
    public Toggle draggingToggle;

    public bool drawing = false;

    private Edge currentEdge;
    private Node currentNode;

    private List<Node> nodes = new List<Node>();
    private List<Edge> edges = new List<Edge>();

    private bool[,] matrix = new bool[20, 20];
    private int currentLine;
    private int currentColumn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (drawing)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos = new Vector3(pos.x, pos.y, 0f);
                currentEdge.lineRenderer.SetPosition(1, pos);
            }
        }

    }

    public void AddNode()
    {
        currentNode = nodePool.GetObject().GetComponent<Node>();
        nodes.Add(currentNode);
        currentNode.InitInfo(nodes.Count - 1, this, draggingToggle);
        currentNode.transform.position = Vector3.zero;

        currentNode = null;
    }

    public void DeleteNode(int nodeIndex)
    {
        currentNode = nodes[nodeIndex];
        nodes.RemoveAt(nodeIndex);
        nodePool.ReturnObject(currentNode.gameObject);

        currentNode = null;
    }

    public void StartEdge(int nodeIndex)
    {
        drawing = true;

        Debug.Log("start edge"+nodeIndex);

        currentEdge = edgePool.GetObject().GetComponent<Edge>();
        currentEdge.lineRenderer.SetPosition(0, nodes[nodeIndex].transform.position);

        currentLine = nodeIndex;

    }

    public void EndEdge(int nodeIndex)
    {
        Debug.Log("end edge"+nodeIndex);

        currentColumn = nodeIndex;
        if (!matrix[currentLine, currentColumn])
        { matrix[currentLine, currentColumn] = true; matrix[currentColumn, currentLine] = true; }

        else{
            edgePool.ReturnObject(currentEdge.gameObject);
            currentEdge = null;
            drawing = false;

            Debug.Log("Edge already exists!");

            return;
        }

        currentEdge.lineRenderer.SetPosition(1, nodes[nodeIndex].transform.position);
        edges.Add(currentEdge);

        currentEdge = null;
        drawing = false;


    }

  
}
