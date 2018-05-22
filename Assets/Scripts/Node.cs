using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private ManagerGraph myManager;
    private Toggle myDraggingToggle; //on when dragging

    private int myIndex;
    private Color myColor;
    private string myLabel;

    private MeshRenderer meshRenderer;
    private Text myText;

	private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myText = GetComponentInChildren<Text>();
    }

	public void InitInfo(int index, ManagerGraph managerGraph, Toggle draggingToggle)
    {
        myManager = managerGraph;
        myDraggingToggle = draggingToggle;

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
    }

	private void OnMouseDrag()
	{
        if (myDraggingToggle.isOn)
        {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos = new Vector3(pos.x, pos.y, 0f);
                transform.position = pos;
        }

        else {
            if (myManager.drawing) return;
            else {
                myManager.StartEdge(myIndex);
            }
        }
	}

	private void OnMouseEnter()
	{
        if(myDraggingToggle.isOn)
        {
            return;
        }
        else if (!myManager.drawing) return;

        myManager.EndEdge(myIndex);
	}

}