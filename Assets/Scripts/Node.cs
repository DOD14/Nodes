using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int myIndex;
    public Color myColor;
    private string myLabel;

    private MeshRenderer meshRenderer;
    private Text myText;

    public List<Edge> myEdges = new List<Edge>();

	private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myText = GetComponentInChildren<Text>();
    }

    public void InitInfo(int index)
    {
        myIndex = index;
        myColor = Color.white;
        myLabel = myIndex.ToString();

        UpdateObjectWithInfo();
    }

    private void UpdateObjectWithInfo()
    {
        myText.text = myLabel;
        meshRenderer.material.color = myColor;
    }

    private void OnMouseDown()
    {
        UpdateObjectWithInfo();

        if (ManagerGraph.instance.deletingToggle.isOn)
        {
            DeleteMeAndMyEdges();
        }
            

    }

    private void OnMouseDrag()
    {
        if (!ManagerGraph.instance.draggingToggle.isOn) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0f);
        transform.position = pos;

        foreach(Edge edge in myEdges)
        {
            edge.UpdatePositions();
        }

       
    }

    private void DeleteMeAndMyEdges()
    {
        ManagerGraph.instance.DeleteNode(this);

        foreach (Edge edge in myEdges)
        {
            ManagerGraph.instance.DeleteEdge(edge);
        }
    }

}