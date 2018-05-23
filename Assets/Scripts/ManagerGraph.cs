using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGraph : MonoBehaviour
{
    public static ManagerGraph instance = null;

    public SimpleObjectPool nodePool;
    public SimpleObjectPool edgePool;

    public Toggle draggingToggle; //on when dragging, off when drawing
    public Toggle deletingToggle; //on when deleting tapped nodes

    public bool drawing = false;

    private Edge currentEdge;

    private Node currentStartNode;
    private Node currentEndNode;

    private List<Node> nodes = new List<Node>();
    private List<Edge> edges = new List<Edge>();

    private bool[,] matrix = new bool[20, 20];
    private int currentLine;
    private int currentColumn;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!draggingToggle.isOn&&Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 20f))
            {
                currentStartNode = hit.collider.gameObject.GetComponent<Node>();
                if(currentStartNode!=null)
                {
                    StartEdge(currentStartNode);
                }

            }
                               
        }

        if (drawing)
        {
            if (Input.GetMouseButton(0)&&currentEdge!=null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos = new Vector3(pos.x, pos.y, 0f);
                currentEdge.lineRenderer.SetPosition(1, pos);
            }

            else if(Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 20f))
                {
                    currentEndNode = hit.collider.gameObject.GetComponent<Node>();
                    if (currentEndNode != null)
                    {
                        EndEdge(currentEndNode);
                    }

                    else{
                        edgePool.ReturnObject(currentEdge.gameObject);
                        currentEdge = null;
                    }

                }

                else {
                    currentStartNode = null;
                    edgePool.ReturnObject(currentEdge.gameObject);
                    currentEdge = null;
                }
            }
        }

    }


    void StartEdge(Node node)
    {
        drawing = true;

        Debug.Log("start edge "+node.myIndex);

        currentEdge = edgePool.GetObject().GetComponent<Edge>();
        currentEdge.lineRenderer.SetPosition(0, node.transform.position);
        currentEdge.SetStartPos(node);

        node.myEdges.Add(currentEdge);

        currentLine = node.myIndex;

    }

    void EndEdge(Node node)
    {
        Debug.Log("end edge"+node.myIndex);

        currentColumn = node.myIndex;

        if (!matrix[currentLine, currentColumn])
        { 
            matrix[currentLine, currentColumn] = true; 
            matrix[currentColumn, currentLine] = true; 

            currentEdge.lineRenderer.SetPosition(1, node.transform.position);
            currentEdge.SetEndPos(node);

            edges.Add(currentEdge);
            node.myEdges.Add(currentEdge);
        }

        else
        {
            currentStartNode.myEdges.Remove(currentEdge);
            edgePool.ReturnObject(currentEdge.gameObject);

            Debug.Log("Edge already exists!");


        }

        drawing = false;

        currentEdge = null;
        currentStartNode = null;
        currentEndNode = null;
    }

    public void AddNode()
    {
        Node currentNode = nodePool.GetObject().GetComponent<Node>();
        nodes.Add(currentNode);
        currentNode.InitInfo(nodes.Count - 1);
        currentNode.transform.position = Vector3.zero;
    }

    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        nodePool.ReturnObject(node.gameObject);
    }

    public void DeleteEdge(Edge edge)
    {
        edges.Remove(edge);
        edgePool.ReturnObject(edge.gameObject);
    }
  
}
